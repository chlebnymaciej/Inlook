using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Storage.Blobs;
using Inlook_Core;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Inlook_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApplicationInsightsTelemetry();

           

            services.Configure<AuthenticationOptions>(Configuration.GetSection("Authentication:AzureAd"));

            IList<string> validissuers = new List<string>()
            {
                Configuration.GetValue<string>("AzureAdB2C:Authority"),
            };

            var configManager = new ConfigurationManager<OpenIdConnectConfiguration>($"{validissuers.Last()}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());

            var openidconfig = configManager.GetConfigurationAsync().Result;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(AppServicesAuthenticationDefaults.AuthenticationScheme, options =>
                    {
                        options.Authority = Configuration.GetValue<string>("AzureAdB2C:Authority");
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateAudience = false,

                            ValidateIssuer = true,
                            ValidIssuers = new[] { Configuration.GetValue<string>("AzureAdB2C:Authority") },

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKeys = openidconfig.SigningKeys,

                            RequireExpirationTime = true,
                            ValidateLifetime = true,
                            RequireSignedTokens = true,
                        };

                        options.RequireHttpsMetadata = false;
                        options.Events = new JwtBearerEvents()
                        {
                            OnTokenValidated = o =>
                            {

                                Guid oid = Guid.Parse(o.Principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier"));

                                var db = o.HttpContext.RequestServices.GetRequiredService<Inlook_Context>();

                                var userInDb = db.Users.Find(oid);
                                if (userInDb == null)
                                {
                                    var userRole = db.Roles.Where(r => r.Name == Roles.Pending).FirstOrDefault();
                                    var givenName = o.Principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
                                    var surName = o.Principal.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname");
                                    var email = o.Principal.FindFirstValue(ClaimTypes.Email);
                                    User user = new User()
                                    {
                                        Id = oid,
                                        Name = givenName + " " + surName,
                                        Email = email,
                                        UserRoles = new List<UserRole> { new UserRole() { RoleId = userRole.Id, UserId = oid } },
                                    };
                                    db.Users.Add(user);
                                    db.SaveChanges();
                                }

                                var userRoles = db.UserRole.Where(ur => ur.UserId == oid).Include(ur => ur.Role).ToList();

                                var claims = new List<Claim>();

                                foreach (var role in userRoles)
                                {
                                    claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                                }

                                var appIdentity = new ClaimsIdentity(claims);

                                o.Principal.AddIdentity(appIdentity);
                                return Task.CompletedTask;
                            },
                        };
                    });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("UserPolicy", p =>
               {
                   p.AuthenticationSchemes.Add(AppServicesAuthenticationDefaults.AuthenticationScheme);
                   p.RequireRole(Roles.User);
               });

                o.AddPolicy("AdminPolicy", p =>
                {
                    p.AuthenticationSchemes.Add(AppServicesAuthenticationDefaults.AuthenticationScheme);
                    p.RequireRole(Roles.Admin);
                });
            });

            services.AddCors();

            services.AddDbContext<Inlook_Context>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureStorageBlobConnectionString")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Inlook API",
                    Version = "v1",
                });

                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

            });

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Inlook API1");
                options.RoutePrefix = "";

            });
        }
    }
}

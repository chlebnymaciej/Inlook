using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
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

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = AzureADDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(AzureADDefaults.AuthenticationScheme, options =>
                {
                    options.Audience = Configuration.GetValue<string>("AzureAdB2C:Audience");
                    options.Authority = Configuration.GetValue<string>("AzureAdB2C:Instance")
                     + Configuration.GetValue<string>("AzureAdB2C:TenantId");
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration.GetValue<string>("AzureAdB2C:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("AzureAdB2C:Audience")
                    };
                    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
                    {
                        OnTokenValidated = o =>
                        {

                            Guid oid = Guid.Parse(o.Principal.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier"));

                            var db = o.HttpContext.RequestServices.GetRequiredService<Inlook_Context>();

                            var userInDb = db.Users.Find(oid);
                            if (userInDb == null)
                            {
                                var userRole = db.Roles.Where(r => r.Name == "User").FirstOrDefault();
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
                   p.AuthenticationSchemes.Add(AzureADDefaults.AuthenticationScheme);
                   p.RequireRole("User");
               });
            });

            services.AddCors();

            services.AddDbContext<Inlook_Context>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureStorageBlobConnectionString")));

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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Inlook_Infrastructure;
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
                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "User")
                        };
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

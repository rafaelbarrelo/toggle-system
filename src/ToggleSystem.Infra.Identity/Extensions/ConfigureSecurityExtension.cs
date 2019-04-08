using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ToggleSystem.Infra.Data.Context;

namespace ToggleSystem.Infra.Identity.Extensions
{
    public static class ConfigureSecurityExtension
    {
        public static IServiceCollection AddSecurityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSecurityContext()
                .AddSecurityAuthentication(configuration)
                .AddSecurityAuthorization();

            return services;
        }

        public static IServiceCollection AddSecurityContext(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ToggleContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddSecurityAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options => {
                options.AddPolicy("CanGetToggle", pp => pp.RequireClaim("Toggle", "Get"));
                options.AddPolicy("CanManage", pp => pp.RequireClaim("Toggle", "Manage"));
            });
            return services;
        }

        public static IServiceCollection AddSecurityAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                var auth = services
                                .AddAuthentication(options =>
                                {
                                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                                })
                                .AddJwtBearer(cfg =>
                                {
                                    cfg.RequireHttpsMetadata = false;
                                    cfg.SaveToken = true;
                                    cfg.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateLifetime = true,
                                        ValidateIssuer = true,
                                        ValidIssuer = configuration["Security:JwtIssuer"],
                                        ValidAudience = configuration["Security:JwtIssuer"],
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:JwtKey"])),
                                        ClockSkew = TimeSpan.Zero
                                    };
                                });
            
            return services;
        }

    }
}

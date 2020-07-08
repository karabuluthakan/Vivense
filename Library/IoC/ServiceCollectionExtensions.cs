using System;
using System.Text;
using Library.Aspects;
using Library.CrossCuttingConcerns.Authorization;
using Library.CrossCuttingConcerns.Authorization.Abstract;
using Library.CrossCuttingConcerns.HealthChecks;
using Library.CrossCuttingConcerns.HealthChecks.Abstract;
using Library.Extensions;
using Library.Utilities.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Library.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy(IoCConstants.AllowAllCorsPolicy,
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(IoCConstants.SetPreflightMaxAge))
                            .WithExposedHeaders(IoCConstants.WWWAuthenticate));
            });
        }

        public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(options =>
            {
                options.Secret = configuration.GetJwtSettingsSecret();
                options.AccessTokenTtlMinutes = configuration.GetJwtSettingsAccessTokenTtlMinutes();
                options.RefreshTokenTtlDays = configuration.GetJwtSettingsRefreshTokenTtlDays();
            });

            var secretKey = Encoding.ASCII.GetBytes(configuration.GetJwtSettingsSecret());
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();
            services.AddHttpContextAccessor();
            services.AddScoped<IEntityHierarchyProvider, EntityHierarchyMemoryProvider>();

            return services;
        }

        public static IServiceCollection AddAspects(this IServiceCollection services)
        {
            return services.AddScoped<ScopeAuthorizeAttribute>();
        }

        public static IServiceCollection AddHealthChecker(this IServiceCollection services)
        {
            services.AddScoped<IMongoDbHealthChecker, MongoDbHealthChecker>();
            return services;
        }
    }
}
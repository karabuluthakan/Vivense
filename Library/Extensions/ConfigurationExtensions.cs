using System;
using Library.Utilities.AppSettings;
using Microsoft.Extensions.Configuration;

namespace Library.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetJwtSettingsSecret(this IConfiguration configuration)
        {
            return configuration.GetSection(nameof(JwtSettings) + ":" + JwtSettings.SecretKey).Value;
        }

        public static int GetJwtSettingsAccessTokenTtlMinutes(this IConfiguration configuration)
        {
            return Convert.ToInt32(configuration.GetSection(nameof(JwtSettings) + ":" + JwtSettings.AccessTokenTtlMinutesKey).Value);
        }

        public static int GetJwtSettingsRefreshTokenTtlDays(this IConfiguration configuration)
        {
            return Convert.ToInt32(configuration.GetSection(nameof(JwtSettings) + ":" + JwtSettings.RefreshTokenTtlDaysKey).Value);
        }
    }
}
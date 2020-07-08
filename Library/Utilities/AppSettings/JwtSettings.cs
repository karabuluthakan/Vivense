namespace Library.Utilities.AppSettings
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int AccessTokenTtlMinutes { get; set; } = 1;
        public int RefreshTokenTtlDays { get; set; } = 2;

        public const string SecretKey = nameof(Secret);
        public const string AccessTokenTtlMinutesKey = nameof(AccessTokenTtlMinutes);
        public const string RefreshTokenTtlDaysKey = nameof(RefreshTokenTtlDays);
    }
}
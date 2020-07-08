namespace Library.Utilities.AppSettings
{
    public class AmazonS3Settings
    {
        public string AccessKey { get; set; }
        public string AccessSecret { get; set; }
        public string Bucket { get; set; }
        public int CacheDurationDays { get; set; } = 30;
        public string EnvironmentSuffix { get; set; }
        public string TempBucket { get; set; }
    }
}
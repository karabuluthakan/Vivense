namespace Library.Utilities.AppSettings
{
    public class KafkaSettings
    {
        public const string BootstrapServersKey = nameof(BootstrapServers);
        public const string TopicSuffixKey = nameof(TopicSuffix);
        public string BootstrapServers { get; set; }
        public string TopicSuffix { get; set; } = null;
    }
}
namespace Library.Utilities.AppSettings
{
    public class ElasticSearchSettings
    {
        public const string UriKey = nameof(Uri);
        public const string IndexSuffixKey = nameof(IndexSuffix);
        public string Uri { get; set; }
        public string IndexSuffix { get; set; } = null;
    }
}
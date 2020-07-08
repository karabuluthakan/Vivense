using Nest;

namespace Library.ElasticSearch
{
    public interface IElasticSearchClientFactory
    {
        IElasticClient ElasticSearchClient { get; }
    }
}
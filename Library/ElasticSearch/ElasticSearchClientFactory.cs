using System;
using System.Text;
using Elasticsearch.Net;
using Library.Utilities.AppSettings;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Library.ElasticSearch
{
    public abstract class ElasticSearchClientFactory : IElasticSearchClientFactory
    {
        public IElasticClient ElasticSearchClient { get; protected set; }

        protected ConnectionSettings GetBasicConnectionSettings(ElasticSearchSettings elasticSearchSettings)
        {
            var pool = new SingleNodeConnectionPool(new Uri(elasticSearchSettings.Uri));
            var connectionSettings = new ConnectionSettings(pool,
                (builtin, settings) =>
                    new JsonNetSerializer(builtin, settings, contractJsonConverters: new JsonConverter[] {new StringEnumConverter()})
            );

            connectionSettings = connectionSettings.DisableDirectStreaming(true);
            connectionSettings = connectionSettings.OnRequestCompleted(call =>
            {
                System.Diagnostics.Debug.WriteLine(Encoding.UTF8.GetString(call.RequestBodyInBytes));
            });

            return connectionSettings;
        }
    }
}
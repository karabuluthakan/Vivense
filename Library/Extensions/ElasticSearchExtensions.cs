using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Library.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static async Task BulkIndexInElasticSearch<T>(this IElasticClient ElasticSearchClient, List<T> elasticSearchRecords)
            where T : class, new()
        {
            const int batchCount = 5000;

            var splitRecordLists = SplitList(elasticSearchRecords, batchCount).ToList();
            
            foreach (var splitRecordsList in splitRecordLists)
            {
                var descriptor = new BulkDescriptor();

                foreach (var elasticSearchRecord in splitRecordsList)
                {
                    descriptor.Index<T>(op => op
                        .Document(elasticSearchRecord)
                    );
                }

                var result = await ElasticSearchClient.BulkAsync(descriptor);
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> items, int chunkSize)
        {
            for (var i = 0; i < items.Count; i += chunkSize)
            {
                yield return items.GetRange(i, Math.Min(chunkSize, items.Count - i));
            }
        }
    }
}
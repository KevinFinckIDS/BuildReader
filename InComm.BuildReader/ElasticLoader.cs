using System;
using System.Collections.Generic;
using System.Globalization;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace InComm.BuildReader
{
    public class ElasticLoader
    {
        private readonly string _elasticSearchAddress;

        public ElasticLoader(string elasticSearchAddress)
        {
            if (string.IsNullOrWhiteSpace(elasticSearchAddress))
            {
                throw new ArgumentNullException("elasticSearchAddress");
            }

            _elasticSearchAddress = elasticSearchAddress;
        }

        public void UploadList<T>(List<T> list, string listName, string modelName)
        {
            var node = new Uri(_elasticSearchAddress);
            var config = new ConnectionConfiguration(node);
            var client = new ElasticsearchClient(config);

            int i = 1;
            list.ForEach(obj =>
            {
                // ReSharper disable once UnusedVariable
                var response = client.Index(
                    listName, 
                    modelName, i.ToString(CultureInfo.InvariantCulture), 
                    obj);
                i++;
            });
        }

    }
}

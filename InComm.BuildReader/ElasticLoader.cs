using System;
using System.Collections.Generic;
using System.Globalization;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace InComm.BuildReader
{
    public static class ElasticLoader
    {
        public static void UploadList<T>(List<T> list, string listName, string modelName)
        {
            var node = new Uri("http://10.3.29.129:9200");
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

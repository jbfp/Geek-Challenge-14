using Nest;
using System;

namespace SGC14.Web.Models.Elasticsearch
{
    public class ElasticClientFactory : IElasticClientFactory
    {
        public IElasticClient Create(Uri uri, string index)
        {
            return new ElasticClient(new ConnectionSettings(uri, index));
        }
    }
}
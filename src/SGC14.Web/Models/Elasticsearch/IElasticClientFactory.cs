using Nest;
using System;

namespace SGC14.Web.Models.Elasticsearch
{
    public interface IElasticClientFactory
    {
        IElasticClient Create(Uri uri, string index);
    }

}
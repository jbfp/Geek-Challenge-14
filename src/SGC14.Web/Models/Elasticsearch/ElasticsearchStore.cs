using Nest;
using System;
using System.Collections.Generic;

namespace SGC14.Web.Models.Elasticsearch
{
    public class ElasticsearchStore : IObserver<ISGC14Item>, IObserver<IList<ISGC14Item>>
    {
        private readonly IElasticClient client;
        private readonly string query;

        public ElasticsearchStore(IElasticClient client, string query)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            this.client = client;
            this.query = query;
        }

        public IEnumerable<T> Search<T>(int page = 0) where T : class, ISGC14Item
        {
            const int hitsPerPage = 15;

            return this.client.Search<T>(
                s => s.QueryString(this.query)
                      .From(page * hitsPerPage)
                      .Size(hitsPerPage)).Documents;
        }

        public void OnNext(ISGC14Item value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.client.Index(value, i => i.Id(value.Id.ToString())
                                           .Type(value.GetType()));
        }

        public void OnNext(IList<ISGC14Item> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (value.Count == 0)
            {
                return;
            }

            this.client.Bulk(b => b.IndexMany(value, (bi, item) => bi.Id(item.Id.ToString())
                                                                     .Type(item.GetType())));
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}
using System;
using Newtonsoft.Json;

namespace SGC14.Web.Models.Dbpedia
{
    public class DbpediaEntry : ISGC14Item
    {
        private readonly string description;
        private readonly string title;

        public DbpediaEntry(string title, string description)
        {
            this.title = title;
            this.description = description;
        }

        [JsonProperty("title")]
        public string Title
        {
            get { return this.title; }
        }

        [JsonProperty("description")]
        public string Description
        {
            get { return this.description; }
        }

        [JsonProperty("id")]
        public object Id
        {
            get { return Title; }
        }

        [JsonProperty("created")]
        public DateTime? Created
        {
            get { return null; }
        }

        [JsonProperty("type")]
        public string Type
        {
            get { return "wiki"; }
        }
    }
}
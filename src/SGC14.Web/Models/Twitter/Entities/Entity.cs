using Newtonsoft.Json;

namespace SGC14.Web.Models.Twitter.Entities
{
    public abstract class Entity
    {
        [JsonProperty("indices")]
        public int[] Indices { get; set; }        
    }
}
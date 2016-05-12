using Newtonsoft.Json;

namespace SGC14.Web.Models.Flickr
{
    public class FlickrImage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("farm")]
        public string Farm { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }        
    }
}
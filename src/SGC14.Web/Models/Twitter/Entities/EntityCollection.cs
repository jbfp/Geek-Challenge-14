using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SGC14.Web.Models.Twitter.Entities
{
    [JsonObject]
    public class EntityCollection : IEnumerable<Entity>
    {
        public EntityCollection()
        {
            Hashtags = new HashtagEntity[0];
            Media = new MediaEntity[0];
            Symbols = new SymbolEntity[0];
            Urls = new UrlEntity[0];
            UserMentions = new UserMentionEntity[0];
        }

        [JsonProperty("hashtags")]        
        public HashtagEntity[] Hashtags { get; set; }

        [JsonProperty("media")]
        public MediaEntity[] Media { get; set; }

        [JsonProperty("symbols")]
        public SymbolEntity[] Symbols { get; set; }

        [JsonProperty("urls")]
        public UrlEntity[] Urls { get; set; }

        [JsonProperty("user_mentions")]
        public UserMentionEntity[] UserMentions { get; set; }

        public IEnumerator<Entity> GetEnumerator()
        {
            return Hashtags.Concat<Entity>(Media)
                           .Concat(Symbols)
                           .Concat(Urls)
                           .Concat(UserMentions)
                           .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
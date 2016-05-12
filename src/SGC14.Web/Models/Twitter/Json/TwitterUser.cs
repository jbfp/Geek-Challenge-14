using Newtonsoft.Json;

namespace SGC14.Web.Models.Twitter.Json
{
    public class TwitterUser
    {
        [JsonProperty("id_str")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps { get; set; }

        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("verified")]
        public bool IsVerified { get; set; }

        public override string ToString()
        {
            return this.Name ?? this.ScreenName ?? this.Id ?? base.ToString();
        }
    }

}
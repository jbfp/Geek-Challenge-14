namespace SGC14.Web.Models.Twitter
{
    public interface ITwitterClientFactory
    {
        ITwitterClient Create(TwitterCredentials credentials);
    }
}
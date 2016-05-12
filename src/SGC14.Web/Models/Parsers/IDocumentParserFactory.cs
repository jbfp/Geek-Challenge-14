namespace SGC14.Web.Models.Parsers
{
    public interface IDocumentParserFactory
    {
        IDocumentParser Get(string host);
    }
}
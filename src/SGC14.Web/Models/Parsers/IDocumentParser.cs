using System.Xml.XPath;
using SGC14.Web.Helpers;

namespace SGC14.Web.Models.Parsers
{
    public interface IDocumentParser
    {
        Maybe<Document> Parse(XPathNavigator navigator);
    }
}
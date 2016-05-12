using System;
using System.Xml.XPath;
using SGC14.Web.Helpers;

namespace SGC14.Web.Models.Parsers
{
    internal abstract class BaseParser : IDocumentParser
    {
        public Maybe<Document> Parse(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException("navigator");
            }

            var title = GetTitle(navigator);

            if (string.IsNullOrWhiteSpace(title))
            {
                return Maybe<Document>.None;
            }

            var description = GetDescription(navigator);
            var publishedDate = GetPublishedDate(navigator);
            var document = new Document(title, description, publishedDate.HasValue ? publishedDate.Value : (DateTime?)null);
            return Maybe<Document>.Some(document);
        }

        protected abstract string GetTitle(XPathNavigator navigator);        

        protected virtual string GetDescription(XPathNavigator navigator)
        {
            return string.Empty;
        }

        protected virtual Maybe<DateTime> GetPublishedDate(XPathNavigator navigator)
        {
            return Maybe<DateTime>.None;
        }
    }
}
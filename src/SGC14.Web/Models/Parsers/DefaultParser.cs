using System.Xml;
using System.Xml.XPath;

namespace SGC14.Web.Models.Parsers
{
    internal class DefaultParser : BaseParser
    {
        protected override string GetTitle(XPathNavigator navigator)
        {
            var title = string.Empty;

            // Try the <meta>-tag with name-attribute = 'title'.
            // Otherwise use the <title>-tag.
            // If that doesn't exist for some odd reason, use the first <h1>.
            var titleNode = navigator.SelectSingleNode("//meta[@name='title']") ?? navigator.SelectSingleNode("//title") ?? navigator.SelectSingleNode("//h1");

            if (titleNode != null)
            {
                // Try the <meta>-tag with name-attribute = 'content'.
                title = titleNode.Value;

                if (string.IsNullOrEmpty(title))
                {
                    title = titleNode.GetAttribute("content", string.Empty);
                }
            }

            return title;
        }

        protected override string GetDescription(XPathNavigator navigator)
        {
            var description = string.Empty;

            // Try selecting a <meta> tag with name 'description', else try with 'og:description' (Facebook OpenGraph).
            // Else try the first <p> tag.                        
            var openGraphNamespaceManager = new XmlNamespaceManager(new NameTable());
            openGraphNamespaceManager.AddNamespace("og", "http://ogp.me/ns#");
            var descriptionNode = navigator.SelectSingleNode("//meta[@name='description']") ??
                                  navigator.SelectSingleNode("//meta[@name='og:description']", openGraphNamespaceManager) ??
                                  navigator.SelectSingleNode("//p");

            if (descriptionNode != null)
            {
                description = descriptionNode.Value;

                if (string.IsNullOrEmpty(description))
                {
                    description = descriptionNode.GetAttribute("content", string.Empty);
                }
            }

            return description;
        }
    }
}
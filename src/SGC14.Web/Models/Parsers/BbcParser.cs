using System;
using System.Globalization;
using System.Xml.XPath;
using SGC14.Web.Helpers;

namespace SGC14.Web.Models.Parsers
{
    [DocumentParser("^(.+\\.)?bbc\\.(in|com|co\\.uk)")]
    internal class BbcParser : DefaultParser
    {
        protected override string GetTitle(XPathNavigator navigator)
        {
            var titleNode = navigator.SelectSingleNode("//meta[@name='Headline']");

            if (titleNode == null)
            {
                return base.GetTitle(navigator);
            }

            return titleNode.GetAttribute("content", string.Empty);
        }

        protected override string GetDescription(XPathNavigator navigator)
        {
            var descriptionNode = navigator.SelectSingleNode("//meta[@name='Description']");
            var description = descriptionNode == null ? base.GetDescription(navigator) : descriptionNode.GetAttribute("content", string.Empty);
            return description;
        }

        protected override Maybe<DateTime> GetPublishedDate(XPathNavigator navigator)
        {
            Maybe<DateTime> publishedDate;

            var datePublishedNode = navigator.SelectSingleNode("//meta[@name='OriginalPublicationDate']");            

            if (datePublishedNode != null)
            {
                var originalPublicationDate = datePublishedNode.GetAttribute("content", string.Empty);

                try
                {
                    publishedDate = DateTime.ParseExact(originalPublicationDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    publishedDate = Maybe<DateTime>.None;
                }                
            }
            else
            {
                publishedDate = base.GetPublishedDate(navigator);
            }

            return publishedDate;
        }
    }
}
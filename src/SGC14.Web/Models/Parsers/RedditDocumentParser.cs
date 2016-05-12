using System;
using System.Globalization;
using System.Text;
using System.Xml.XPath;
using SGC14.Web.Helpers;

namespace SGC14.Web.Models.Parsers
{
    [DocumentParser("^redd.it")]
    [DocumentParser("^(.+\\.)?reddit.com")]
    internal class RedditDocumentParser : DefaultParser
    {
        protected override string GetTitle(XPathNavigator navigator)
        {
            var titleNode = navigator.SelectSingleNode("//a[contains(@class, 'title')]");
            return titleNode == null ? base.GetTitle(navigator) : titleNode.Value;
        }

        protected override string GetDescription(XPathNavigator navigator)
        {
            var descriptionNode = navigator.SelectSingleNode("//div[contains(@class, 'md')]");
            var descriptionBuilder = new StringBuilder();

            if (descriptionNode != null)
            {
                var paragraphNodes = descriptionNode.SelectDescendants("p", string.Empty, false);

                while (paragraphNodes.MoveNext())
                {
                    descriptionBuilder.AppendLine(paragraphNodes.Current.Value);
                }
            }
            else
            {
                return base.GetDescription(navigator);                
            }

            return descriptionBuilder.ToString();
        }

        protected override Maybe<DateTime> GetPublishedDate(XPathNavigator navigator)
        {
            Maybe<DateTime> publishedDate;
            var timeNode = navigator.SelectSingleNode("//time");            

            if (timeNode != null)
            {
                var datetime = timeNode.GetAttribute("datetime", string.Empty);

                try
                {
                    var publishedDateTimeOffset = DateTimeOffset.ParseExact(datetime, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
                    publishedDate = publishedDateTimeOffset.DateTime;
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
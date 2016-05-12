using System;
using System.Globalization;
using System.Xml.XPath;
using SGC14.Web.Helpers;

namespace SGC14.Web.Models.Parsers
{
    [DocumentParser("^(.+\\.)?cnn.(com|it)")]
    internal class CnnParser : DefaultParser
    {
        protected override string GetTitle(XPathNavigator navigator)
        {
            var titleNode = navigator.SelectSingleNode("//meta[@name='title']");
            return titleNode == null ? base.GetTitle(navigator) : titleNode.GetAttribute("content", string.Empty);
        }

        protected override string GetDescription(XPathNavigator navigator)
        {
            var descriptionNode = navigator.SelectSingleNode("//meta[@name='description']");
            return descriptionNode == null ? base.GetDescription(navigator) : descriptionNode.GetAttribute("content", string.Empty);
        }

        protected override Maybe<DateTime> GetPublishedDate(XPathNavigator navigator)
        {
            Maybe<DateTime> publishedDate;
            var pubdateNode = navigator.SelectSingleNode("//meta[@name='pubdate']");

            if (pubdateNode != null)
            {
                var pubdate = pubdateNode.GetAttribute("content", string.Empty);

                try
                {
                    publishedDate = DateTime.ParseExact(pubdate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
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
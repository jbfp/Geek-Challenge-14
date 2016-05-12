using System;
using System.Globalization;
using System.Xml.XPath;
using SGC14.Web.Helpers;

namespace SGC14.Web.Models.Parsers
{
    [DocumentParser("^nyti.ms")]
    [DocumentParser("^(.+\\.)?nytimes.com")]
    internal class NewYorkTimesParser : DefaultParser
    {
        protected override string GetTitle(XPathNavigator navigator)
        {
            var titleNode = navigator.SelectSingleNode("//meta[@name='hdl_p']");
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
            var createdNode = navigator.SelectSingleNode("//meta[@name='ptime']");

            if (createdNode != null)
            {
                var ptime = createdNode.GetAttribute("content", string.Empty);

                try
                {
                    publishedDate = DateTime.ParseExact(ptime, "yyyMMddHHmmss", CultureInfo.InvariantCulture);                    
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
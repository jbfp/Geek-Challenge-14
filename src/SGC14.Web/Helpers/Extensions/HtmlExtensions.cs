using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SGC14.Web.Helpers.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlString SelectLanguage(this HtmlHelper htmlHelper, string name, string optionLabel, object htmlAttributes)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            var options = from culture in GetUniqueCultureInfos()
                          let text = culture.DisplayName
                          let value = culture.TwoLetterISOLanguageName
                          orderby text
                          select new SelectListItem { Text = text, Value = value };

            return htmlHelper.DropDownList(name, options, optionLabel, htmlAttributes);
        }

        internal static IEnumerable<CultureInfo> GetUniqueCultureInfos()
        {
            return CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                              .Distinct(new TwoLetterIsoLanguageNameEqualityComparer());
        }
    }

    internal class TwoLetterIsoLanguageNameEqualityComparer : IEqualityComparer<CultureInfo>
    {
        public bool Equals(CultureInfo x, CultureInfo y)
        {
            if (ReferenceEquals(null, x))
            {
                return false;
            }

            if (ReferenceEquals(null, y))
            {
                return false;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return string.Equals(x.TwoLetterISOLanguageName, y.TwoLetterISOLanguageName);
        }

        public int GetHashCode(CultureInfo obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.TwoLetterISOLanguageName.GetHashCode();
        }
    }
}
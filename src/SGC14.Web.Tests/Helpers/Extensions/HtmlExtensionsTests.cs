using Moq;
using NUnit.Framework;
using SGC14.Web.Helpers.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGC14.Web.Tests.Helpers.Extensions
{
    [TestFixture]
    public class HtmlExtensionsTests
    {
        [Test]
        public void GetUniqueCultureInfos_returns_unique_cultures()
        {
            var actual = HtmlExtensions.GetUniqueCultureInfos();
            var grouped = actual.GroupBy(c => c.TwoLetterISOLanguageName);
            Assert.That(grouped.All(g => g.Count() == 1));
        }

        [Test]
        public void SelectLanguage_throws_ArgumentNullException_when_htmlHelper_is_null()
        {
            TestDelegate test = () => HtmlExtensions.SelectLanguage(null, "language", "All languages", new { });
            Assert.That(test, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void SelectLanguage_builds_select_list()
        {
            // Arrange.
            var viewData = new ViewDataDictionary();
            var sut = CreateHtmlHelper<dynamic>(viewData);
            
            // Act.
            var actual = sut.SelectLanguage("language", "All languages", new { });

            // Assert.
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToHtmlString(), Is.EqualTo(SelectListHtml));
        }

        internal HtmlHelper<T> CreateHtmlHelper<T>(ViewDataDictionary viewData)
        {
            var mockControllerContext = new Mock<ControllerContext>(
                Mock.Of<HttpContextBase>(),
                new RouteData(),
                Mock.Of<ControllerBase>());

            var mockViewContext = new Mock<ViewContext>(
                mockControllerContext.Object,
                Mock.Of<IView>(),
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);
            mockViewContext.Setup(v => v.ViewData).Returns(viewData);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            return new HtmlHelper<T>(mockViewContext.Object, mockViewDataContainer.Object);
        }

        private const string SelectListHtml = @"<select id=""language"" name=""language""><option value="""">All languages</option>
<option value=""af"">Afrikaans</option>
<option value=""sq"">Albanian</option>
<option value=""gsw"">Alsatian</option>
<option value=""am"">Amharic</option>
<option value=""ar"">Arabic</option>
<option value=""hy"">Armenian</option>
<option value=""as"">Assamese</option>
<option value=""az"">Azeri</option>
<option value=""jv"">Basa Jawa</option>
<option value=""ba"">Bashkir</option>
<option value=""eu"">Basque</option>
<option value=""be"">Belarusian</option>
<option value=""bn"">Bengali</option>
<option value=""bs"">Bosnian</option>
<option value=""br"">Breton</option>
<option value=""bg"">Bulgarian</option>
<option value=""my"">Burmese</option>
<option value=""ca"">Catalan</option>
<option value=""ku"">Central Kurdish</option>
<option value=""chr"">Cherokee</option>
<option value=""zh"">Chinese</option>
<option value=""sn"">chiShona</option>
<option value=""co"">Corsican</option>
<option value=""hr"">Croatian</option>
<option value=""cs"">Czech</option>
<option value=""da"">Danish</option>
<option value=""prs"">Dari</option>
<option value=""dv"">Divehi</option>
<option value=""nl"">Dutch</option>
<option value=""en"">English</option>
<option value=""et"">Estonian</option>
<option value=""fo"">Faroese</option>
<option value=""fil"">Filipino</option>
<option value=""fi"">Finnish</option>
<option value=""fr"">French</option>
<option value=""fy"">Frisian</option>
<option value=""ff"">Fulah</option>
<option value=""gl"">Galician</option>
<option value=""ka"">Georgian</option>
<option value=""de"">German</option>
<option value=""el"">Greek</option>
<option value=""kl"">Greenlandic</option>
<option value=""gn"">Guarani</option>
<option value=""gu"">Gujarati</option>
<option value=""ha"">Hausa</option>
<option value=""haw"">Hawaiian</option>
<option value=""he"">Hebrew</option>
<option value=""hi"">Hindi</option>
<option value=""hu"">Hungarian</option>
<option value=""is"">Icelandic</option>
<option value=""ig"">Igbo</option>
<option value=""id"">Indonesian</option>
<option value=""iu"">Inuktitut</option>
<option value=""iv"">Invariant Language (Invariant Country)</option>
<option value=""ga"">Irish</option>
<option value=""xh"">isiXhosa</option>
<option value=""zu"">isiZulu</option>
<option value=""it"">Italian</option>
<option value=""ja"">Japanese</option>
<option value=""kn"">Kannada</option>
<option value=""kk"">Kazakh</option>
<option value=""km"">Khmer</option>
<option value=""qut"">K&#39;iche</option>
<option value=""rw"">Kinyarwanda</option>
<option value=""sw"">Kiswahili</option>
<option value=""kok"">Konkani</option>
<option value=""ko"">Korean</option>
<option value=""ky"">Kyrgyz</option>
<option value=""lo"">Lao</option>
<option value=""lv"">Latvian</option>
<option value=""lt"">Lithuanian</option>
<option value=""dsb"">Lower Sorbian</option>
<option value=""lb"">Luxembourgish</option>
<option value=""mk"">Macedonian (FYROM)</option>
<option value=""mg"">Malagasy</option>
<option value=""ms"">Malay</option>
<option value=""ml"">Malayalam</option>
<option value=""mt"">Maltese</option>
<option value=""mi"">Maori</option>
<option value=""arn"">Mapudungun</option>
<option value=""mr"">Marathi</option>
<option value=""moh"">Mohawk</option>
<option value=""mn"">Mongolian</option>
<option value=""ne"">Nepali</option>
<option value=""nb"">Norwegian (Bokm&#229;l)</option>
<option value=""nn"">Norwegian (Nynorsk)</option>
<option value=""oc"">Occitan</option>
<option value=""or"">Oriya</option>
<option value=""om"">Oromo</option>
<option value=""ps"">Pashto</option>
<option value=""fa"">Persian</option>
<option value=""pl"">Polish</option>
<option value=""pt"">Portuguese</option>
<option value=""pa"">Punjabi</option>
<option value=""quz"">Quechua</option>
<option value=""ro"">Romanian</option>
<option value=""rm"">Romansh</option>
<option value=""ru"">Russian</option>
<option value=""sah"">Sakha</option>
<option value=""smn"">Sami (Inari)</option>
<option value=""smj"">Sami (Lule)</option>
<option value=""se"">Sami (Northern)</option>
<option value=""sms"">Sami (Skolt)</option>
<option value=""sma"">Sami (Southern)</option>
<option value=""sa"">Sanskrit</option>
<option value=""gd"">Scottish Gaelic</option>
<option value=""sr"">Serbian</option>
<option value=""nso"">Sesotho sa Leboa</option>
<option value=""tn"">Setswana</option>
<option value=""sd"">Sindhi</option>
<option value=""si"">Sinhala</option>
<option value=""sk"">Slovak</option>
<option value=""sl"">Slovenian</option>
<option value=""so"">Somali</option>
<option value=""st"">Southern Sotho</option>
<option value=""es"">Spanish</option>
<option value=""zgh"">Standard Morrocan Tamazight</option>
<option value=""sv"">Swedish</option>
<option value=""syr"">Syriac</option>
<option value=""tg"">Tajik</option>
<option value=""tzm"">Tamazight</option>
<option value=""ta"">Tamil</option>
<option value=""tt"">Tatar</option>
<option value=""te"">Telugu</option>
<option value=""th"">Thai</option>
<option value=""bo"">Tibetan</option>
<option value=""ti"">Tigrinya</option>
<option value=""ts"">Tsonga</option>
<option value=""tr"">Turkish</option>
<option value=""tk"">Turkmen</option>
<option value=""uk"">Ukrainian</option>
<option value=""hsb"">Upper Sorbian</option>
<option value=""ur"">Urdu</option>
<option value=""ug"">Uyghur</option>
<option value=""uz"">Uzbek</option>
<option value=""vi"">Vietnamese</option>
<option value=""cy"">Welsh</option>
<option value=""wo"">Wolof</option>
<option value=""ii"">Yi</option>
<option value=""yo"">Yoruba</option>
<option value=""nqo"">ߒߞߏ</option>
</select>";
    }
}
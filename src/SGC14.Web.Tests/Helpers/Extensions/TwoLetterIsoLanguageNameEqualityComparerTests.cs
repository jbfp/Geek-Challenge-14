using NUnit.Framework;
using SGC14.Web.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SGC14.Web.Tests.Helpers.Extensions
{
    [TestFixture]
    public class TwoLetterIsoLanguageNameEqualityComparerTests
    {
        [Test]
        public void Sut_is_IEqualityComparerOfCultureInfo()
        {
            var sut = new TwoLetterIsoLanguageNameEqualityComparer();
            Assert.That(sut, Is.AssignableTo<IEqualityComparer<CultureInfo>>());
        }

        [Test]
        public void GetHashCode_throws_ArgumentNullException_when_obj_is_null()
        {
            // Arrange.
            var sut = new TwoLetterIsoLanguageNameEqualityComparer();

            // Act.
            TestDelegate test = () => sut.GetHashCode(null);

            // Assert.
            Assert.That(test, Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetHashCode_returns_hash_code_of_two_letter_ISO_language_name()
        {
            var cultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var sut = new TwoLetterIsoLanguageNameEqualityComparer();

            foreach (var culture in cultureInfo)
            {
                var expected = culture.TwoLetterISOLanguageName.GetHashCode();
                var actual = sut.GetHashCode(culture);
                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [Test]
        public void Equals_returns_false_if_either_argument_is_null()
        {
            // Arrange.
            var cultureInfo = CultureInfo.InvariantCulture;
            var sut = new TwoLetterIsoLanguageNameEqualityComparer();

            // Act.
            var left = sut.Equals(null, cultureInfo);
            var right = sut.Equals(cultureInfo, null);

            // Assert.
            Assert.That(left, Is.False);
            Assert.That(right, Is.False);
        }

        [Test]
        public void Equals_returns_true_if_argument_is_the_same_reference()
        {
            // Arrange.
            var cultureInfo = CultureInfo.InvariantCulture;
            var sut = new TwoLetterIsoLanguageNameEqualityComparer();

            // Act.
            var actual = sut.Equals(cultureInfo, cultureInfo);

            // Assert.
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Equals_returns_true_if_arguments_have_the_same_two_letter_ISO_language_name()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            var cultureGroups = cultures.GroupBy(ci => ci.TwoLetterISOLanguageName);
            var sut = new TwoLetterIsoLanguageNameEqualityComparer();

            foreach (var cultureGroup in cultureGroups)
            {
                // Assert that every permutation in this group e.g. en-US + en-GB, en-US + en-AU are the same ("en".)
                var permutations = cultureGroup.Zip(cultureGroup, sut.Equals);
                Assert.That(permutations.All(b => b));
            }
        }
    }
}
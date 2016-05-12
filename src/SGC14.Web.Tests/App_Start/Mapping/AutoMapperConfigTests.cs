using AutoMapper;
using NUnit.Framework;
using SGC14.Web.Mapping;

namespace SGC14.Web.Tests.Mapping
{
    [TestFixture]
    public class AutoMapperConfigTests
    {
        [Test]
        public void AutoMapper_configuration_is_correct()
        {
            AutoMapperConfig.Configure();
            Mapper.AssertConfigurationIsValid();
        }
    }
}
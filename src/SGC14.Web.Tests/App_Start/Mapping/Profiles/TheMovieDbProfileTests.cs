﻿using AutoMapper;
using NUnit.Framework;
using SGC14.Web.Mapping.Profiles;

namespace SGC14.Web.Tests.Mapping.Profiles
{
    [TestFixture]
    public class TheMovieDbProfileTests
    {
        [Test]
        public void Configuration_is_valid()
        {
            Mapper.AssertConfigurationIsValid<TheMovieDbProfile>();
        }
    }
}
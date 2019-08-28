using System;
using Shouldly;
using Xunit;
using static ei_infrastructure.Utils.StringUtils;

namespace ei_unit_tests.Infrastructure.StringUtilsTests
{
    public class TestRandomString
    {
        [Fact]
        public void ReturnedRandomStringsAreUnique()
        {
            const int expectedLength = 5;
            const int numberOfStringsToGenerate = 100;
            var strings = new string[numberOfStringsToGenerate];
            for (var i = 0; i < numberOfStringsToGenerate; i++)
                strings[i] = RandomString(expectedLength);
            strings.ShouldBeUnique();
        }

        [Fact]
        public void ReturnsARandomString()
        {
            const int expectedLength = 20;
            var randomString = RandomString(expectedLength);
            randomString.ShouldNotBeNullOrWhiteSpace();
            randomString.Length.ShouldBe(expectedLength);
        }

        [Fact]
        public void ThrowsAnExceptionWhenLengthIsAboveTheAllowedRange()
        {
            const int expectedLength = 1000000;
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => RandomString(expectedLength));
            ex.Message.ShouldContain("length");
        }

        [Fact]
        public void ThrowsAnExceptionWhenLengthIsBelowTheAllowedRange()
        {
            const int expectedLength = 0;
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => RandomString(expectedLength));
            ex.Message.ShouldContain("length");
        }
    }
}
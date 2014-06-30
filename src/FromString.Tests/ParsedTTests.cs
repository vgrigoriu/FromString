using System;
using Xunit;

namespace FromString.Tests
{
    public class ParsedTTests
    {
        [Fact]
        public void CanParseAnInt()
        {
            var parsedInt = new Parsed<int>("123");

            Assert.True(parsedInt.HasValue);
            Assert.Equal(123, parsedInt.Value);
        }

        [Fact]
        public void AnInvalidStringSetsHasValueFalse()
        {
            var parsedInt = new Parsed<int>("this is not a string");

            Assert.False(parsedInt.HasValue);
        }

        [Fact]
        public void AnInvalidStringThrowsExceptionWhenAccessingValue()
        {
            var parsedInt = new Parsed<int>("this is not a string");

            Assert.Throws<InvalidOperationException>(() => parsedInt.Value);
        }

        [Fact]
        public void CanGetBackRawValue()
        {
            var parsedInt = new Parsed<int>("this is not a string");

            Assert.Equal("this is not a string", parsedInt.RawValue);
        }

        [Fact]
        public void CanAssignValueDirectly()
        {
            Parsed<decimal> directDecimal = 123.45m;

            Assert.True(directDecimal.HasValue);
            Assert.Equal(123.45m, directDecimal.Value);
        }

        [Fact]
        public void ParsingInvalidUriFails()
        {
            var parsedUri = new Parsed<Uri>("this is not an URI");

            Assert.False(parsedUri.HasValue);
        }
    }
}

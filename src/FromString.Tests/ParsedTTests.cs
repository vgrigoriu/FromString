﻿using System;
using System.Linq;

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

        [Fact]
        public void ParsingValidAbsoluteUriSucceeds()
        {
            var parsedUri = new Parsed<Uri>("https://github.com/");

            Assert.True(parsedUri.HasValue);
            Assert.Equal(new Uri("https://github.com/"), parsedUri.Value);
        }

        private class C
        {
            public string P { get; set; }
        }

        [Fact]
        public void CanAddCustomParser()
        {
            Parsed<C>.AddTryParse(
                (string s, out C c) =>
                    {
                        c = new C { P = s.ToUpperInvariant() };
                        return true;
                    });

            var parsedC = new Parsed<C>("some string");

            Assert.True(parsedC.HasValue);
            Assert.Equal("SOME STRING", parsedC.Value.P);
        }

        [Fact]
        public void ToStringReturnsValueToStringForValidParses()
        {
            var validParsedInt = new Parsed<int>("7");

            Assert.Equal("7", validParsedInt.ToString());
        }

        [Fact]
        public void ToStringReturnsRawValueForInvalidParses()
        {
            var invalidParsedInt = new Parsed<int>("not an int");

            Assert.Equal("not an int", invalidParsedInt.ToString());
        }
    }
}

using Xunit;

namespace FromString.Tests
{
    public class ParsedDoubleTests
    {
        [Fact]
        public void InvalidDoubleIsNotParsed()
        {
            var invalidParsedDouble = new Parsed<double>("not a valid double");

            Assert.False(invalidParsedDouble.HasValue);
        }

        [Fact]
        public void ValidDoubleIsParsed()
        {
            var validParsedDouble = new Parsed<double>("-72.45");

            Assert.True(validParsedDouble.HasValue);
            Assert.Equal(-72.45, validParsedDouble.Value);
        }
    }
}

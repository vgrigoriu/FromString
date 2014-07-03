using Xunit;

namespace FromString.Tests
{
    public class ParsedEnumTests
    {
        [Fact]
        public void InvalidEnumIsNotParsed()
        {
            var parsedEnum = new Parsed<Color>("invalid enum value");

            Assert.False(parsedEnum.HasValue);
        }

        [Fact]
        public void ValidEnumIsParsed()
        {
            var validParsedEnum = new Parsed<Color>("Blue");

            Assert.True(validParsedEnum.HasValue);
            Assert.Equal(Color.Blue, validParsedEnum.Value);
        }

        private enum Color
        {
            Red,
            Green,
            Blue
        }
    }
}

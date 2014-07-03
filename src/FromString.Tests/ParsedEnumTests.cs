using Xunit;

namespace FromString.Tests
{
    public class ParsedEnumTests
    {
        [Fact]
        public void InvalidEnumIsNotParsed()
        {
            var invalidParsedEnum = new Parsed<Color>("invalid enum value");

            Assert.False(invalidParsedEnum.HasValue);
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

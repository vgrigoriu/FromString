using Xunit;

namespace FromString.Tests
{
    public class ParsedEnumTests
    {
        [Fact(Skip = "Need a little refactoring first")]
        public void InvalidEnumIsNotParsed()
        {
            var parsedEnum = new Parsed<Color>("invalid enum value");

            Assert.False(parsedEnum.HasValue);
        }

        private enum Color
        {
            Red,
            Green,
            Blue
        }
    }
}

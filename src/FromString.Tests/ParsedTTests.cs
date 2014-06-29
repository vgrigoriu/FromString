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
    }
}

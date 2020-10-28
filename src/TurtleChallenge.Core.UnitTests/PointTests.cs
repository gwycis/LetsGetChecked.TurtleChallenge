using TurtleChallenge.Core.Domain;
using Xunit;

namespace TurtleChallenge.Core.UnitTests
{
    public class PointTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public void Given_Coordinates_When_Valid_Then_CreatePoint(int x, int y)
        {
            // Arrange
            // Act
            var result = new Point(x , y);

            // Assert
            Assert.Equal(result.X, x);
            Assert.Equal(result.Y, y);
        }

        [Theory]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        [InlineData(-1, 0)]
        [InlineData(int.MinValue, int.MinValue)]
        public void Given_Coordinates_When_OutOfRange_Then_Throw(int x, int y)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<TurtleException>(() => new Point(x, y));
        }
    }
}
using TurtleChallenge.Core.Domain;
using Xunit;

namespace TurtleChallenge.Core.UnitTests
{
    public sealed class TurtleTests
    {
        [Theory]
        [InlineData(Directions.North, 0, 0)]
        [InlineData(Directions.North, 10, 10)]
        [InlineData(Directions.East, 0, 0)]
        [InlineData(Directions.East, 10, 10)]
        [InlineData(Directions.South, 0, 0)]
        [InlineData(Directions.South, 10, 10)]
        [InlineData(Directions.West, 0, 0)]
        [InlineData(Directions.West, 10, 10)]
        public void Given_Parameters_When_Valid_Then_CreateTurtle(Directions direction, int x, int y)
        {
            // Arrange
            var position = new Point(x, y);

            // Act
            var result = new Turtle(direction, position);
            
            // Assert
            Assert.Equal(direction, result.Direction);
            Assert.Equal(position, result.Position);
        }  
        
        [Theory]
        [InlineData(Directions.North, 1, Directions.East)]
        [InlineData(Directions.East, 1, Directions.South)]
        [InlineData(Directions.South, 1, Directions.West)]
        [InlineData(Directions.West, 1, Directions.North)]
        [InlineData(Directions.North, 2, Directions.South)]
        [InlineData(Directions.East, 2, Directions.West)]
        [InlineData(Directions.South, 2, Directions.North)]
        [InlineData(Directions.West, 2, Directions.East)]
        [InlineData(Directions.North, 4, Directions.North)]
        [InlineData(Directions.East, 4, Directions.East)]
        [InlineData(Directions.South, 4, Directions.South)]
        [InlineData(Directions.West, 4, Directions.West)]
        public void Given_Turtle_When_Rotated_Then_ReturnTurtleWithUpdatedDirection(Directions direction, int rotations, Directions expectedDirection)
        {
            // Arrange
            var position = new Point(5, 5);
            var sut = new Turtle(direction, position);

            // Act
            for (int i = 0; i < rotations; i++)
                sut = sut.Rotate();

            // Assert
            Assert.Equal(position, sut.Position);
            Assert.Equal(expectedDirection, sut.Direction);
        }
        
        [Theory]
        [InlineData(Directions.North, 1, 1, 1, 0)]
        [InlineData(Directions.East, 0, 0, 1, 0)]
        [InlineData(Directions.South, 0, 0, 0, 1)]
        [InlineData(Directions.West, 1, 1, 0, 1)]
        public void Given_Turtle_When_Moved_Then_ReturnTurtleWithUpdatedPosition(Directions direction, 
            int startingX, int startingY, int expectedX, int expectedY)
        {
            // Arrange
            var position = new Point(startingX, startingY);
            var expected = new Point(expectedX, expectedY);
            var sut = new Turtle(direction, position);

            // Act
            sut = sut.Move();

            // Assert
            Assert.Equal(expected, sut.Position);
            Assert.Equal(direction, sut.Direction);
        }
    }
}
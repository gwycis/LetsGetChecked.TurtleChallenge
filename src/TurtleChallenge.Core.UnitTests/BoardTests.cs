using System.Collections.Generic;
using TurtleChallenge.Core.Domain;
using Xunit;

namespace TurtleChallenge.Core.UnitTests
{
    public sealed class BoardTests
    {
        [Theory]
        [InlineData(1, 2, 1, 0, 0, 0)]
        [InlineData(2, 1, 0, 1, 0, 0)]
        [InlineData(10, 10, 0, 0, 9, 9)]
        [InlineData(10, 10, 5, 5, 4, 4)]
        [InlineData(100, 100, 0, 0, 99, 99)]
        [InlineData(100, 100, 50, 50, 75, 75)]
        [InlineData(100, 100, 99, 99, 50, 50)]
        public void Given_BoardParameters_When_Valid_Then_ReturnBoard(int rows, int columns, int exitX, int exitY, int mineX, int mineY)
        {
            // Arrange
            var exit = new Point(exitX, exitY);
            var mine = new Point(mineX, mineY);

            // Act
            var result = new Board(rows, columns, new HashSet<Point> { mine }, exit);

            // Assert
            Assert.Equal(rows, result.Rows);
            Assert.Equal(columns, result.Columns);
            Assert.True(result.GetCell(exit).IsExit);
            Assert.True(result.GetCell(mine).HasMine);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public void Given_BoardParameters_When_RowOrColumnConfigurationIsInvalid_Then_Throw(int rows, int columns)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<TurtleException>(() => new Board(rows, columns, new HashSet<Point>(), new Point(0, 0)));
        }

        [Theory]
        [InlineData(10, 10, 0, 10)]
        [InlineData(10, 10, 10, 0)]
        public void Given_BoardParameters_When_SomeMinesAreOutOfBounds_Then_Throw(int rows, int columns, int mineX, int mineY)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<TurtleException>(() => new Board(rows, columns, new HashSet<Point> { new Point(mineX, mineY) }, new Point(0, 0)));
        }

        [Theory]
        [InlineData(10, 10, 0, 10)]
        [InlineData(10, 10, 10, 0)]
        public void Given_BoardParameters_When_ExitIsOutOfBounds_Then_Throw(int rows, int columns, int exitX, int exitY)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<TurtleException>(() => new Board(rows, columns, new HashSet<Point>(), new Point(exitX, exitY)));
        }

        [Theory]
        [InlineData(10, 10, 0, 0)]
        [InlineData(10, 10, 5, 5)]
        [InlineData(10, 10, 9, 9)]
        public void Given_BoardParameters_When_ExitOverlapsWithMine_Then_Throw(int rows, int columns, int x, int y)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<TurtleException>(() => new Board(rows, columns, new HashSet<Point>
            {
                new Point(1, 1),
                new Point(x, y),
                new Point(9, 4)
            }, new Point(x, y)));
        }

        [Theory]
        [InlineData(10, 10, 10, 0)]
        [InlineData(10, 10, 0, 10)]
        public void Given_Board_When_CellRequested_But_IsOutOfBounds_Then_Throw(int rows, int columns, int x, int y)
        {
            // Arrange
            var sut = new Board(rows, columns, new HashSet<Point>(), new Point(9, 9));
            var point = new Point(x, y);

            // Act
            // Assert
            Assert.Throws<TurtleException>(() => sut.GetCell(point));
        }

        [Theory]
        [InlineData(10, 10, 10, 0, false)]
        [InlineData(10, 10, 0, 10, false)]
        [InlineData(10, 10, 0, 9, true)]
        [InlineData(10, 10, 9, 0, true)]
        [InlineData(10, 10, 0, 0, true)]
        [InlineData(10, 10, 9, 9, true)]
        public void Given_Board_When_CellRequested_Then_ReturnIfSucCellExists(int rows, int columns, int x, int y, bool exists)
        {
            // Arrange
            var sut = new Board(rows, columns, new HashSet<Point>(), new Point(9, 9));
            var point = new Point(x, y);

            // Act
            // Assert
            Assert.Equal(exists, sut.HasCell(point));
        }
    }
}
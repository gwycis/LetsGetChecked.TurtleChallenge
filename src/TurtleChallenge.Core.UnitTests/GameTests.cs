using System.Collections.Generic;
using TurtleChallenge.Core.Domain;
using Xunit;

namespace TurtleChallenge.Core.UnitTests
{
    public sealed class GameTests
    {
        [Theory]
        [InlineData(5, 5)]
        [InlineData(5, 4)]
        [InlineData(4, 5)]
        public void Given_Game_When_TurtleStartingPositionIsOutOfBounds_Then_Return(int x, int y)
        {
            // Arrange
            var board = new Board(5, 5, new HashSet<Point>(), new Point(0, 0));
            var startingPoint = new Point(x, y);
            var turtle = new Turtle(Directions.East, startingPoint);
            var sut = new Game(board, turtle);
            
            // Act
            var result = sut.Play(new List<Moves>());

            // Assert
            Assert.Equal(startingPoint, result.FinalPosition);
            Assert.Equal($"Turtle has an out of bounds starting point {startingPoint} given a 5x5 board.", result.Message);
        }

        [Theory]
        [MemberData(nameof(SuccessTestCases))]
        public void Given_Game_When_SequencePlacesTurtleOnExit_Then_ReturnSuccess(List<Moves> sequence)
        {
            // Arrange
            var board = new Board(4, 5, new HashSet<Point>
            {
                new Point(1, 1),
                new Point(3, 1),
                new Point(3, 3)
            }, new Point(4, 2));
            
            var turtle = new Turtle(Directions.North, new Point(0, 1));
            var sut = new Game(board, turtle);
           
            // Act
            var result = sut.Play(sequence);

            // Assert
            Assert.Equal(new Point(4, 2), result.FinalPosition);
            Assert.Equal("Success!", result.Message);
        }

        [Theory]
        [MemberData(nameof(MineTestCases))]
        public void Given_Game_When_SequencePlacesTurtleOnMine_Then_ReturnMineHit(List<Moves> sequence, Point finalPosition)
        {
            // Arrange
            var board = new Board(4, 5, new HashSet<Point>
            {
                new Point(1, 1),
                new Point(3, 1),
                new Point(3, 3)
            }, new Point(4, 2));
            
            var turtle = new Turtle(Directions.North, new Point(0, 1));
            var sut = new Game(board, turtle);
          
            // Act
            var result = sut.Play(sequence);

            // Assert
            Assert.Equal(finalPosition, result.FinalPosition);
            Assert.Equal("Mine hit!", result.Message);
        }

        [Theory]
        [MemberData(nameof(DangerTestCases))]
        public void Given_Game_When_SequenceNotReachingExit_Then_ReturnStillInDanger(List<Moves> sequence, Point finalPosition)
        {
            // Arrange
            var board = new Board(4, 5, new HashSet<Point>
            {
                new Point(1, 1),
                new Point(3, 1),
                new Point(3, 3)
            }, new Point(4, 2));
            
            var turtle = new Turtle(Directions.North, new Point(0, 1));
            var sut = new Game(board, turtle);
          
            // Act
            var result = sut.Play(sequence);

            // Assert
            Assert.Equal(finalPosition, result.FinalPosition);
            Assert.Equal("Still in danger!", result.Message);
        }

        [Theory]
        [MemberData(nameof(OutOfBoundsCases))]
        public void Given_Game_When_SequenceResultsInOutOfBoundsPosition_Then_Throw(List<Moves> sequence)
        {
            // Arrange
            var board = new Board(4, 5, new HashSet<Point>
            {
                new Point(1, 1),
                new Point(3, 1),
                new Point(3, 3)
            }, new Point(4, 2));
            
            var turtle = new Turtle(Directions.North, new Point(0, 1));
            var sut = new Game(board, turtle);
          
            // Act
            var result = sut.Play(sequence);

            // Assert
            Assert.StartsWith("Oops!", result.Message);
        }


        public static IEnumerable<object[]> MineTestCases =>
            new List<object[]>
            {
                new object[] { new List<Moves> { Moves.Rotate, Moves.Go }, new Point(1, 1)},
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go }, new Point(3, 1)},
                new object[] { new List<Moves> { Moves.Rotate, Moves.Rotate, Moves.Go, Moves.Rotate, Moves.Rotate, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go }, new Point(3, 3)},
            };

        public static IEnumerable<object[]> SuccessTestCases =>
            new List<object[]>
            {
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go, Moves.Go } },
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Rotate, Moves.Rotate, Moves.Rotate, Moves.Go, Moves.Go } },
                new object[] { new List<Moves> { Moves.Rotate, Moves.Rotate, Moves.Go, Moves.Rotate, Moves.Rotate, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Go } }
            };

        public static IEnumerable<object[]> DangerTestCases =>
            new List<object[]>
            {
                new object[] { new List<Moves> {  }, new Point(0, 1)},
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go }, new Point(4, 1) },
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go }, new Point(4, 3) },
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Rotate, Moves.Rotate, Moves.Rotate, Moves.Go }, new Point(3, 2) }
            };

        public static IEnumerable<object[]> OutOfBoundsCases =>
            new List<object[]>
            {
                new object[] { new List<Moves> { Moves.Go, Moves.Go } },
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go, Moves.Go } },
                new object[] { new List<Moves> { Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Rotate, Moves.Go, Moves.Go, Moves.Rotate, Moves.Rotate, Moves.Rotate, Moves.Go, Moves.Go, Moves.Go } }
            };
    }
}
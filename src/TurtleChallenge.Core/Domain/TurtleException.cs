using System;
using System.Collections.Generic;

namespace TurtleChallenge.Core.Domain
{
    public sealed class TurtleException : Exception
    {
        public TurtleException(string message) : base(message)
        {
        }

        public static TurtleException WhenCoordinateOutOfRange(string name) =>
            new TurtleException($"{name} must be equal 0 or more");

        public static TurtleException WhenRowsOutOfRange() => new TurtleException("Board must have at least 1 ROW.");

        public static TurtleException WhenColumnsOutOfRange() =>
            new TurtleException("Board must have at least 1 COLUMN.");

        public static TurtleException WhenMinesAreOutOfBounds(int rows, int columns, IEnumerable<Point> mines) =>
            new TurtleException(
                $"Board has {rows} rows and {columns} columns, following mines are out of bounds: {string.Join(", ", mines)}");

        public static TurtleException WhenExitIsOutOfBounds(int rows, int columns, Point exit) =>
            new TurtleException(
                $"Board has {rows} rows and {columns} columns, but given EXIT is out of bounds: {exit}");

        public static TurtleException WhenExitHasMine(Point exit) =>
            new TurtleException(
                $"Board exit is at {exit}, however the cell also has a MINE. Cell must be either EXIT or MINE or SAFE!");

        public static TurtleException WhenPointOutOfBounds(int rows, int columns, Point point) =>
            new TurtleException(
                $"Board has {rows} rows and {columns} columns, but given point {point} is outside board boundaries!");
    }
}
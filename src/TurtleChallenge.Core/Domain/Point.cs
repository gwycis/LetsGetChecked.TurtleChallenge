using System;

namespace TurtleChallenge.Core.Domain
{
    public sealed class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            if (x < 0) throw TurtleException.WhenCoordinateOutOfRange(nameof(x));
            if (y < 0) throw TurtleException.WhenCoordinateOutOfRange(nameof(y));
            X = x;
            Y = y;
        }

        #region Equality

        public bool Equals(Point other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Point other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Point left, Point right) => Equals(left, right);

        public static bool operator !=(Point left, Point right) => !Equals(left, right);

        #endregion

        public override string ToString() => $"X: {X}, Y: {Y}";
    }
}
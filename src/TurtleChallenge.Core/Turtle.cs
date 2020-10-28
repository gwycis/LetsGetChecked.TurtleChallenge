using System;
using System.ComponentModel;
using TurtleChallenge.Core.Domain;

namespace TurtleChallenge.Core
{
    public sealed class Turtle
    {
        public Turtle(Directions direction, Point position)
        {
            if (!Enum.IsDefined(typeof(Directions), direction))
                throw new InvalidEnumArgumentException(nameof(direction), (int) direction, typeof(Directions));
            Direction = direction;
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public Directions Direction { get; }

        public Point Position { get; }

        public Turtle Rotate()
        {
            var newDirection = Direction switch
            {
                Directions.North => Directions.East,
                Directions.East => Directions.South,
                Directions.South => Directions.West,
                Directions.West => Directions.North,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new Turtle(newDirection, Position);
        }

        public Turtle Move()
        {
            var x = Position.X;
            var y = Position.Y;

            if (Direction == Directions.North)
                y = y - 1;
            if (Direction == Directions.South)
                y = y + 1;
            if (Direction == Directions.West)
                x = x - 1;
            if (Direction == Directions.East)
                x = x + 1;

            return new Turtle(Direction, new Point(x , y));
        }

        public override string ToString() => $"Turtle faces {Direction} and is located at {Position}";
    }
}
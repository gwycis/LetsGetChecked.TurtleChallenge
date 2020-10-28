using System;

namespace TurtleChallenge.Core.Domain
{
    public sealed class Cell
    {
        public string State { get; }
        public bool HasMine => State == Mine;
        public bool IsExit => State == Exit;
        public bool IsSafe => State == Safe;

        private const string Mine = "Mine";
        private const string Safe = "Safe";
        private const string Exit = "Exit";

        private Cell(string state)
        {
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(state));
            State = state;
        }

        public static Cell CreateMine() => new Cell(Mine);
        public static Cell CreateSafe() => new Cell(Safe);
        public static Cell CreateExit() => new Cell(Exit);

    }
}
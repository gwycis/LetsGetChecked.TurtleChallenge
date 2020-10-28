using System;

namespace TurtleChallenge.Core.Domain
{
    public sealed class GameResult
    {
        public string Message { get; }
        public Point FinalPosition { get; }

        public GameResult(string message, Point finalPosition)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));
            Message = message;
            FinalPosition = finalPosition ?? throw new ArgumentNullException(nameof(finalPosition));
        }
    }
}
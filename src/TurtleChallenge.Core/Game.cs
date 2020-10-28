using System;
using System.Collections.Generic;
using TurtleChallenge.Core.Domain;

namespace TurtleChallenge.Core
{
    public sealed class Game
    {
        private readonly Board _board;
        private readonly Turtle _turtle;

        public Game(Board board, Turtle turtle)
        {
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _turtle = turtle ?? throw new ArgumentNullException(nameof(turtle));
        }

        public GameResult Play(IReadOnlyList<Moves> sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (_board.HasCell(_turtle.Position) == false)
                return new GameResult($"Turtle has an out of bounds starting point {_turtle.Position} given a {_board.Rows}x{_board.Columns} board.", _turtle.Position);

            var turtle = _turtle;

            try
            {
                foreach (var move in sequence)
                {
                    if (move == Moves.Rotate)
                    {
                        turtle = turtle.Rotate();
                        continue;
                    }

                    turtle = turtle.Move();

                    if (_board.HasCell(turtle.Position))
                    {
                        if (_board.GetCell(turtle.Position).HasMine)
                            return new GameResult("Mine hit!", turtle.Position);
                    }
                }

                var cell = _board.GetCell(turtle.Position);
                if (cell.IsExit)
                    return new GameResult("Success!", turtle.Position);

                return new GameResult("Still in danger!", turtle.Position);
            }
            catch (TurtleException e)
            {
                return new GameResult($"Oops! {e.Message}", turtle.Position);
            }
        }
    }
}

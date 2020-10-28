using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Core.Domain;

namespace TurtleChallenge.Core
{
    public sealed class Board
    {
        private readonly Cell[,] _tiles;

        public int Rows => _tiles.GetLength(1);
        public int Columns => _tiles.GetLength(0);

        public Board(int rows, int columns, HashSet<Point> mines, Point exit)
        {
            if (rows <= 0) throw TurtleException.WhenRowsOutOfRange();
            if (columns <= 0) throw TurtleException.WhenColumnsOutOfRange();
            if (mines == null) throw new ArgumentNullException(nameof(mines));
            if (exit == null) throw new ArgumentNullException(nameof(exit));

            _tiles = new Cell[columns, rows];

            var minesOutOfBounds = mines.Where(OutOfBounds).ToList();
            if (minesOutOfBounds.Any())
                throw TurtleException.WhenMinesAreOutOfBounds(rows, columns, minesOutOfBounds);

            if (OutOfBounds(exit))
                throw TurtleException.WhenExitIsOutOfBounds(rows, columns, exit);

            if (mines.Contains(exit))
                throw TurtleException.WhenExitHasMine(exit);


            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var point = new Point(x ,y);
                    if (exit == point)
                        _tiles[x, y] = Cell.CreateExit();
                    else if (mines.Contains(point))
                        _tiles[x, y] = Cell.CreateMine();
                    else
                        _tiles[x, y] = Cell.CreateSafe();
                }
            } 
        }

        private bool OutOfBounds(Point point) => WithinBounds(point) == false;
        private bool WithinBounds(Point point) => point != null && point.Y < Rows && point.X < Columns;

        public Cell GetCell(Point point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            if (OutOfBounds(point)) throw TurtleException.WhenPointOutOfBounds(Rows, Columns, point);

            return _tiles[point.X, point.Y];
        }

        public bool HasCell(Point point) => WithinBounds(point);
    }
}
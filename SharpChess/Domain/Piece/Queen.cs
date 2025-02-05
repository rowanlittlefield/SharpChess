﻿using System.Collections.Generic;

namespace SharpChess
{
    public class Queen : Piece
    {
        private static readonly (int, int)[] MOVE_DIFFS = {
            (1, 0),
            (1, -1),
            (0, -1),
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, 1),
            (1, 1),
        };

        public Queen(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override void Accept(PieceVisitor visitor)
        {
            visitor.VisitQueen(this);
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return SlidingPathFinder.GetMoveOptions(board, this, MOVE_DIFFS);
        }
    }
}

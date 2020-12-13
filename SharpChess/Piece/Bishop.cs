﻿using System.Collections.Generic;

namespace SharpChess
{
    public class Bishop : Piece
    {
        public Bishop(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "b";
        }

        public override void Move()
        {

        }

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

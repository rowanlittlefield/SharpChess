﻿using System.Collections.Generic;

namespace SharpChess
{
    abstract public class Piece
    {
        public readonly PieceColor color;

        public Piece(PieceColor color)
        {
            this.color = color;
        }

        public abstract string Render();

        public abstract HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates);

        public abstract void Move();
    }
}

﻿using System.Collections.Generic;

namespace SharpChess
{
    abstract public class Piece
    {
        public readonly PieceColor Color;
        public (int, int) Coordinates { get; private set; }

        public Piece(PieceColor color, (int, int) coordinates)
        {
            Color = color;
            Coordinates = coordinates;
        }

        public abstract HashSet<(int, int)> GetMoveOptions(Board board);

        public abstract void Accept(PieceVisitor visitor);

        public virtual void Move((int, int) coordinates)
        {
            Coordinates = coordinates;
        }

        public bool IsNullPiece()
        {
            return this == NullPiece.GetInstance();
        }

        public virtual Piece Clone()
        {
            return (Piece)MemberwiseClone();
        }
    }
}

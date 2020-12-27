using System.Collections.Generic;

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

        public abstract string Render();

        public abstract HashSet<(int, int)> GetMoveOptions(Board board);

        public virtual void Move((int, int) coordinates)
        {
            Coordinates = coordinates;
        }

        public virtual void UndoMove((int, int) coordinates)
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

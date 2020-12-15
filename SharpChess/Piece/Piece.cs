using System.Collections.Generic;

namespace SharpChess
{
    abstract public class Piece
    {
        public readonly PieceColor color;
        public (int, int) Coordinates { get; private set; }

        public Piece(PieceColor color, (int, int) coordinates)
        {
            this.color = color;
            Coordinates = coordinates;
        }

        public abstract string Render();

        public abstract HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates);

        public virtual void Move((int, int) coordinates)
        {
            Coordinates = coordinates;
        }

        public bool IsNullPiece()
        {
            return this == NullPiece.GetInstance();
        }
    }
}

using System.Collections.Generic;

namespace SharpChess
{
    public sealed class NullPiece : Piece
    {
        private static NullPiece instance = null;

        private NullPiece() : base(PieceColor.Null, (-1, -1))
        {
        }

        public static NullPiece GetInstance()
        {
            if (instance == null)
            {
                instance = new NullPiece();
            }

            return instance;
        }

        public override string Render()
        {
            return " ";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

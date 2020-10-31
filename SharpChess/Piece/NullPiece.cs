using System;
namespace SharpChess
{
    public sealed class NullPiece : Piece
    {
        private static NullPiece instance = null;

        private NullPiece()
        {
            color = PieceColor.Null;
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
    }
}

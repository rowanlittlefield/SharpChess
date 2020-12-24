using System;
namespace SharpChess
{
    public abstract class PieceRenderer
    {
        public PieceRenderer()
        {
        }

        public abstract void Render(Piece piece, bool isCursorPos, PieceSelection pieceSelection, (int, int) pos);
    }
}

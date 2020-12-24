namespace SharpChess
{
    public abstract class PieceView
    {
        public PieceView()
        {
        }

        public abstract void Render(Piece piece, bool isCursorPos, PieceSelection pieceSelection, (int, int) pos);
    }
}

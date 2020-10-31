namespace SharpChess
{
    abstract public class Piece
    {
        public PieceColor color;

        public Piece()
        {
        }

        public abstract string Render();
    }
}

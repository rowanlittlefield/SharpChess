namespace SharpChess
{
    abstract public class Piece
    {
        public PieceColor color;

        public Piece(PieceColor color)
        {
            this.color = color;
        }

        public abstract string Render();
    }
}

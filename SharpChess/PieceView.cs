namespace SharpChess
{
    public abstract class PieceView
    {
        public PieceView()
        {
        }

        public abstract void Render(Board board, (int, int) pos);
    }
}

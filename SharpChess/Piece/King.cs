namespace SharpChess
{
    public class King : Piece
    {
        public King(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "K";
        }
    }
}

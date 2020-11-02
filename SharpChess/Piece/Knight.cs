namespace SharpChess
{
    public class Knight : Piece
    {
        public Knight(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "k";
        }
    }
}

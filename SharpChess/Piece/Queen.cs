namespace SharpChess
{
    public class Queen : Piece
    {
        public Queen(PieceColor color) : base(color) 
        {
        }

        public override string Render()
        {
            return "Q";
        }
    }
}

namespace SharpChess
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "p";
        }
    }
}

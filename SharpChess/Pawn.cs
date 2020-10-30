using System;
namespace SharpChess
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor color)
        {
            this.color = color;
        }

        public override string Render()
        {
            return "p";
        }
    }
}

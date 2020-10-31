using System;
namespace SharpChess
{
    public class Bishop : Piece
    {
        public Bishop(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "R";
        }
    }
}

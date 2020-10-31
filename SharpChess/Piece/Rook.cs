using System;
namespace SharpChess
{
    public class Rook : Piece
    {
        public Rook(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "r";
        }
    }
}

using System.Collections.Generic;

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

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

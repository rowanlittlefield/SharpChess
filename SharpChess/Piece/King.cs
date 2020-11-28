using System.Collections.Generic;

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

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

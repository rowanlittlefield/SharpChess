using System.Collections.Generic;

namespace SharpChess
{
    public class King : Piece
    {
        public King(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "K";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

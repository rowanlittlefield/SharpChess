using System.Collections.Generic;

namespace SharpChess
{
    public class Queen : Piece
    {
        public Queen(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "Q";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

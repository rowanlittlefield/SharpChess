using System.Collections.Generic;

namespace SharpChess
{
    public class Bishop : Piece
    {
        private static readonly (int, int)[] MOVE_DIFFS = {
            (1, -1),
            (1, 1),
            (-1, 1),
            (-1, -1),
        };

        public Bishop(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "b";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return SlidingPathFinder.GetMoveOptions(board, this, MOVE_DIFFS);
        }
    }
}

using System.Collections.Generic;

namespace SharpChess
{
    public class Queen : Piece
    {
        private static readonly (int, int)[] MOVE_DIFFS = {
            (1, 0),
            (1, -1),
            (0, -1),
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, 1),
            (1, 1),
        };

        public Queen(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "Q";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return SlidingPathFinder.GetMoveOptions(board, this, MOVE_DIFFS);
        }

        public override Piece Clone()
        {
            return new Queen(Color, (Coordinates.Item1, Coordinates.Item2));
        }
    }
}

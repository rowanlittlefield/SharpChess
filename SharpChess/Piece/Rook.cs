using System.Collections.Generic;

namespace SharpChess
{
    public class Rook : Piece
    {
        private static readonly (int, int)[] MOVE_DIFFS = {
            (1, 0),
            (0, 1),
            (-1, 0),
            (0, -1),
        };

        public Rook(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "r";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            return SlidingPathFinder.GetMoveOptions(board, this, MOVE_DIFFS);
        }

        public override Piece Clone()
        {
            return new Rook(Color, (Coordinates.Item1, Coordinates.Item2));
        }
    }
}

using System.Collections.Generic;

namespace SharpChess
{
    public class King : Piece
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

        public King(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "K";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            var moveOptions = new HashSet<(int, int)> { };
            var (row, col) = Coordinates;
            foreach (var (rowDiff, colDiff) in MOVE_DIFFS)
            {
                var position = (row + rowDiff, col + colDiff);
                var validMove = board.IsOnBoard(position)
                    && board.GetPiece(position).Color != Color;
                if (validMove)
                {
                    moveOptions.Add(position);
                }
            }

            return moveOptions;
        }
    }
}

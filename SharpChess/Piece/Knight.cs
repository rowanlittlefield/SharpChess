using System.Collections.Generic;

namespace SharpChess
{
    public class Knight : Piece
    {
        private static readonly (int, int)[] MOVE_DIFFS = {
            (2, 1),
            (2, -1),
            (1, -2),
            (-1, -2),
            (-2, -1),
            (-2, 1),
            (-1, 2),
            (1, 2),
        };

        public Knight(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "k";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            var moveOptions = new HashSet<(int, int)> { };
            var (row, col) = Coordinates;
            foreach ((int, int) diff in MOVE_DIFFS)
            {
                var position = (diff.Item1 + row, diff.Item2 + col);
                var isValidMove = board.IsOnBoard(position) && board.GetPiece(position).color != this.color;
                if (isValidMove)
                {
                    moveOptions.Add(position);
                }
            }
            return moveOptions;
        }
    }
}

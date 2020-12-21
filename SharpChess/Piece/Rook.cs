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
            var moveOptions = new HashSet<(int, int)> { };
            foreach ((int, int) moveDiff in MOVE_DIFFS)
            {
                var movesInDirection = _getMovesInDirection(board, moveDiff);
                foreach ((int, int) position in movesInDirection)
                {
                    moveOptions.Add(position);
                }
            }

            return moveOptions;
        }

        private HashSet<(int, int)> _getMovesInDirection(Board board, (int, int) moveDiff)
        {
            var moveOptions = new HashSet<(int, int)> {};
            var position = Coordinates;
            var (rowDiff, colDiff) = moveDiff;

            var isPathClear = true;
            while (isPathClear)
            {
                position = (position.Item1 + rowDiff, position.Item2 + colDiff);
                var invalidMove = !board.IsOnBoard(position)
                    || board.GetPiece(position).color == this.color;
                if (invalidMove)
                {
                    isPathClear = false;
                }
                else
                {
                    moveOptions.Add(position);
                    isPathClear = board.GetPiece(position).IsNullPiece();
                }
            }

            return moveOptions;
        }
    }
}

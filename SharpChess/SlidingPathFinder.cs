using System.Collections.Generic;
namespace SharpChess
{
    public static class SlidingPathFinder
    {
        static public HashSet<(int, int)> GetMoveOptions(Board board, Piece piece, (int, int)[] moveDiffs)
        {
            var moveOptions = new HashSet<(int, int)> { };
            foreach (var moveDiff in moveDiffs)
            {
                var movesInDirection = _getMovesInDirection(board, piece, moveDiff);
                foreach (var position in movesInDirection)
                {
                    moveOptions.Add(position);
                }
            }

            return moveOptions;
        }

        private static HashSet<(int, int)> _getMovesInDirection(Board board, Piece piece, (int, int) moveDiff)
        {
            var moveOptions = new HashSet<(int, int)> { };
            var position = piece.Coordinates;
            var (rowDiff, colDiff) = moveDiff;

            var isPathClear = true;
            while (isPathClear)
            {
                position = (position.Item1 + rowDiff, position.Item2 + colDiff);
                var isValidMove = board.IsOnBoard(position)
                    && board.GetPiece(position).Color != piece.Color;
                if (isValidMove)
                {
                    moveOptions.Add(position);
                }
 
                isPathClear = isValidMove && board.GetPiece(position).IsNullPiece();
            }

            return moveOptions;
        }
    }
}

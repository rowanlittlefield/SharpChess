using System.Collections.Generic;

namespace SharpChess
{
    public class Knight : Piece
    {
        public static readonly (int, int)[] MOVE_DIFFS = {
            (2, 1),
            (2, -1),
            (1, -2),
            (-1, -2),
            (-2, -1),
            (-2, 1),
            (-1, 2),
            (1, 2),
        };

        public Knight(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "k";
        }

        public override void Move()
        {
        
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            var moveOptions = new HashSet<(int, int)> { };

            foreach ((int, int) diff in MOVE_DIFFS)
            {
                var position = (
                    diff.Item1 + coordinates.Item1,
                    diff.Item2 + coordinates.Item2
                );

                if (board.IsValidMove(position, color))
                {
                    moveOptions.Add(position);
                }
            }
            return moveOptions;
        }
    }
}

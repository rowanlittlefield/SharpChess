using System.Collections.Generic;

namespace SharpChess
{
    public class Rook : Piece
    {
        public Rook(PieceColor color) : base(color)
        {
        }

        public override string Render()
        {
            return "r";
        }

        public override void Move()
        {

        }

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

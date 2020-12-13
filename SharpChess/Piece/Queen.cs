using System.Collections.Generic;

namespace SharpChess
{
    public class Queen : Piece
    {
        public Queen(PieceColor color) : base(color) 
        {
        }

        public override string Render()
        {
            return "Q";
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

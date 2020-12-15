using System.Collections.Generic;

namespace SharpChess
{
    public class Bishop : Piece
    {
        public Bishop(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
        }

        public override string Render()
        {
            return "b";
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            return new HashSet<(int, int)> { };
        }
    }
}

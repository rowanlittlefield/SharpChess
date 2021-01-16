using System.Collections.Generic;

namespace SharpChess
{
    public class PieceSelection
    {
        public readonly HashSet<(int, int)> moveOptions;
        public readonly Piece piece;

        public PieceSelection(Piece piece, HashSet<(int, int)> moveOptions)
        {
            this.piece = piece;
            this.moveOptions = moveOptions;
        }
    }
}

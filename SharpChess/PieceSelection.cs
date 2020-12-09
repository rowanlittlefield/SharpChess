using System.Collections.Generic;

namespace SharpChess
{
    public class PieceSelection
    {
        public readonly (int, int) coordinates;
        public readonly HashSet<(int, int)> moveOptions;
        public readonly Piece piece;

        public PieceSelection((int, int) coordinates, HashSet<(int, int)> moveOptions, Piece piece)
        {
            this.coordinates = coordinates;
            this.moveOptions = moveOptions;
            this.piece = piece;
        }
    }
}

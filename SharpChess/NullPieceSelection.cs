using System.Collections.Generic;

namespace SharpChess
{
    public sealed class NullPieceSelection : PieceSelection
    {
        private static NullPieceSelection instance = null;

        private NullPieceSelection() : base((-1, -1), new HashSet<(int, int)> {}, NullPiece.GetInstance())
        {
        }

        public static NullPieceSelection GetInstance()
        {
            if (instance == null)
            {
                instance = new NullPieceSelection();
            }

            return instance;
        }
    }
}

using System.Collections.Generic;

namespace SharpChess
{
    public sealed class NullPieceSelection : PieceSelection
    {
        private static NullPieceSelection instance = null;

        private NullPieceSelection() : base(NullPiece.GetInstance(), new HashSet<(int, int)> {})
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

using System.Collections.Generic;

namespace SharpChess
{
    public sealed class NullPieceSelection : PieceSelection
    {
        private static NullPieceSelection _instance = null;

        private NullPieceSelection() : base(NullPiece.GetInstance(), new HashSet<(int, int)> {})
        {
        }

        public static NullPieceSelection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NullPieceSelection();
            }

            return _instance;
        }
    }
}

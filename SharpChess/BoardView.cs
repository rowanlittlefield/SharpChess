using System;
namespace SharpChess
{
    public class BoardView
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private PieceView[] _themes = new PieceView[]
        {
            new DefaultPieceView(),
            new PictoralPieceView(),
        };
        private int _pieceViewIndex;
        private PieceView _pieceView;

        public BoardView()
        {
            _pieceViewIndex = 0;
            _pieceView = _themes[_pieceViewIndex];
        }

        public void Render(Board board, (int, int) cursorPos, PieceSelection pieceSelection)
        {
            for (int i = 0; i < Board.GridLength; i += 1)
            {
                for (int j = 0; j < Board.GridLength; j += 1)
                {
                    var pos = (i, j);
                    var piece = board.GetPiece(pos);
                    var isCursorPos = pos == cursorPos;
                    _pieceView.Render(piece, isCursorPos, pieceSelection, pos);
                }

                Console.WriteLine("");
            }
        }

        public void ToggleTheme()
        {
            _pieceViewIndex = (_pieceViewIndex + 1) % _themes.Length;
            _pieceView = _themes[_pieceViewIndex];
        }
    }
}

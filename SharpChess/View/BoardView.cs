using System;
namespace SharpChess
{
    public class BoardView : View
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private PieceView[] _themes = new PieceView[]
        {
            new DefaultPieceView(),
            new PictoralPieceView(),
        };
        private Board _board;
        private int _pieceViewIndex;
        private PieceView _pieceView;

        public BoardView(Board board)
        {
            _board = board;
            _pieceViewIndex = 0;
            _pieceView = _themes[_pieceViewIndex];
        }

        public override void Render()
        {
            var board = _board;
            var isFlipped = board.IsFlipped;
            var initialRowIndex = isFlipped ? GridLength - 1 : 0;
            var rowIncrementerIndex = isFlipped ? -1 : 1;
            for (int row = initialRowIndex; row < GridLength && row >= 0; row += rowIncrementerIndex * 1)
            {
                for (int col = 0; col < GridLength; col += 1)
                {
                    var pos = (row, col);
                    _pieceView.Render(board, pos);
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

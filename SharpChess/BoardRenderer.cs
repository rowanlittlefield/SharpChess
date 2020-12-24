using System;
namespace SharpChess
{
    public class BoardRenderer
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private PieceRenderer[] _themes = new PieceRenderer[]
        {
            new DefaultPieceRenderer(),
            new PictoralPieceRenderer(),
        };
        private int _pieceRendererIndex;
        private PieceRenderer _pieceRenderer;

        public BoardRenderer()
        {
            _pieceRendererIndex = 0;
            _pieceRenderer = _themes[_pieceRendererIndex];
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
                    _pieceRenderer.Render(piece, isCursorPos, pieceSelection, pos);
                }

                Console.WriteLine("");
            }
        }

        public void ToggleTheme()
        {
            _pieceRendererIndex = (_pieceRendererIndex + 1) % _themes.Length;
            _pieceRenderer = _themes[_pieceRendererIndex];
        }
    }
}

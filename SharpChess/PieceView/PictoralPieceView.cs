using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class PictoralPieceView : PieceView
    {
        private static Dictionary<Type, char> PIECE_MAP = new Dictionary<Type, char>
        {
            { typeof(King), '\u2654' },
            { typeof(Queen), '\u2655' },
            { typeof(Rook), '\u2656' },
            { typeof(Bishop), '\u2657' },
            { typeof(Knight), '\u2658' },
            { typeof(Pawn), '\u2659' },
            { typeof(NullPiece), ' ' },
        };

        public PictoralPieceView()
        {
        }

        public override void Render(Board board, (int, int) pos)
        {
            var piece = board.GetPiece(pos);
            var isCursorPos = board.GetCursorCoordinates() == pos;
            if (isCursorPos)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (board.PieceSelection.moveOptions.Contains(pos))
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (board.PieceSelection.piece.Coordinates == pos)
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            }
            else if (_isColoredBackgroundPosition(pos))
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }

            if (piece.Color == PieceColor.Black)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }

            if (piece.Color == PieceColor.White)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.Write(" ");
            _renderPiece(piece);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ResetColor();
        }

        private bool _isColoredBackgroundPosition((int, int) pos)
        {
            var (row, col) = pos;
            return (row % 2 == 0 && col % 2 == 1)
                || (row % 2 == 1 && col % 2 == 0);
        }

        private void _renderPiece(Piece piece)
        {
            var pieceCharacter = PictoralPieceView.PIECE_MAP.GetValueOrDefault(piece.GetType());
            Console.Write(pieceCharacter);
        }
    }
}

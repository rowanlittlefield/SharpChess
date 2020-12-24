using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class PictoralPieceRenderer : PieceRenderer
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

        public PictoralPieceRenderer()
        {
        }

        public override void Render(Piece piece, bool isCursorPos, PieceSelection pieceSelection, (int, int) pos)
        {
            if (isCursorPos)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (pieceSelection.moveOptions.Contains(pos))
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (pieceSelection.piece.Coordinates == pos)
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

            _renderPiece(piece);
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
            var pieceCharacter = PictoralPieceRenderer.PIECE_MAP.GetValueOrDefault(piece.GetType());
            Console.Write(pieceCharacter);
        }
    }
}

﻿using System;
using System.Collections.Generic;
namespace SharpChess
{
    public class DefaultPieceRenderer : PieceRenderer
    {
        private static Dictionary<Type, char> PIECE_MAP = new Dictionary<Type, char>
        {
            { typeof(King), 'K' },
            { typeof(Queen), 'Q' },
            { typeof(Rook), 'r' },
            { typeof(Bishop), 'b' },
            { typeof(Knight), 'k' },
            { typeof(Pawn), 'p' },
            { typeof(NullPiece), ' ' },
        };

        public DefaultPieceRenderer()
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

            Console.Write("[");

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
            Console.Write("]");
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
            var pieceCharacter = DefaultPieceRenderer.PIECE_MAP.GetValueOrDefault(piece.GetType());
            Console.Write(pieceCharacter);
        }
    }

}

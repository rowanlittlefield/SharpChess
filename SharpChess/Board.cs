using System;
using System.Linq;

namespace SharpChess
{
    public class Board
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private Cursor cursor;
        private Piece[,] grid;
        private PieceSelection pieceSelection;

        public Board()
        {
            cursor = new Cursor(Board.GridLength);
            grid = new GridBuilder().CreateGrid();
            pieceSelection = NullPieceSelection.GetInstance();
        }

        public bool GameOver(PieceColor currentPlayer)
        {
            return false;
        }

        public bool IsInCheck(PieceColor playerColor)
        {
            var kingPosition = (0, 0);
            foreach (var piece in grid)
            {
                var isKing = piece is King && piece.Color == playerColor;
                if (isKing)
                {
                    kingPosition = piece.Coordinates;
                }
            }

            var opponentPieceQuery = from Piece piece in grid
                                     let isNull = piece.Color == PieceColor.Null
                                     let isOwn = piece.Color == playerColor
                                     let isOpponentPiece = !isNull && !isOwn
                                     where isOpponentPiece
                                     select piece;

            return opponentPieceQuery
                .Any(piece => piece.GetMoveOptions(this).Contains(kingPosition));
        }

        public void Render()
        {
            var cursorPos = cursor.getCoordinates();
            for (int i = 0; i < Board.GridLength; i += 1)
            {
                for(int j = 0; j < Board.GridLength; j += 1)
                {
                    var pos = (i, j);
                    if (cursorPos == pos)
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

                    var piece = grid[i, j];
                    if (piece.Color == PieceColor.Black)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }

                    if (piece.Color == PieceColor.White)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(piece.Render());
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("]");

                    Console.ResetColor();
                }

                Console.WriteLine("");
            }
        }

        public bool MoveCursor(UserAction userAction)
        {
            cursor.Move(userAction);
            return false;
        }

        public bool SelectCursorPosition(PieceColor currentPlayer)
        {
            if (pieceSelection.moveOptions.Contains(cursor.getCoordinates()))
            {
                _moveSelectedPiece(cursor.getCoordinates());
                return true;
            }
            
            _selectCursorPiece(currentPlayer);
            return false;
        }

        private void _selectCursorPiece(PieceColor currentPlayer)
        {
            var coordinates = cursor.getCoordinates();
            var piece = grid[coordinates.Item1, coordinates.Item2];
            var isCurrentPlayerPiece = piece.Color == currentPlayer;
            if (isCurrentPlayerPiece)
            {
                var moveOptions = piece.GetMoveOptions(this);
                pieceSelection = new PieceSelection(piece, moveOptions);
            }
            else
            {
                pieceSelection = NullPieceSelection.GetInstance();
            }
        }

        private void _moveSelectedPiece((int, int) coordinates)
        {
            var (oldRow, oldCol) = pieceSelection.piece.Coordinates;
            var (row, col) = coordinates;
            pieceSelection.piece.Move(coordinates);
            grid[row, col] = pieceSelection.piece;
            grid[oldRow, oldCol] = NullPiece.GetInstance();
            pieceSelection = NullPieceSelection.GetInstance();
        }

        public bool IsOnBoard((int, int) position)
        {
            var (row, col) = position;
            var isRowOnBoard = row >= 0 && row < GridLength;
            var isColOnBoard = col >= 0 && col < GridLength;

            return isRowOnBoard && isColOnBoard;
        }

        public Piece GetPiece((int, int) coordinates)
        {
            var (row, col) = coordinates;
            return grid[row, col];
        }
    }
}

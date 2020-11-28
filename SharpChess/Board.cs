using System;
using System.Collections.Generic;
using System.Threading;

namespace SharpChess
{
    public class Board
    {
        public static readonly int GridLength = 8;
        private Cursor cursor;
        private Piece[,] grid;
        private PieceSelection pieceSelection;

        public Board()
        {
            cursor = new Cursor(Board.GridLength);
            grid = CreateGrid();

            var coordinates = (0, 0);
            var moveOptions = new HashSet<(int, int)> { };
            var piece = NullPiece.GetInstance();
            pieceSelection = new PieceSelection(coordinates, moveOptions, piece);
        }

        public bool GameOver()
        {
            return false;
        }

        public void Render()
        {
            var cursorPos = cursor.getCoordinates();
            for (int i = 0; i < Board.GridLength; i += 1)
            {
                for(int j = 0; j < Board.GridLength; j += 1)
                {
                    var isCursorPos = cursorPos == (i, j);
                    if (isCursorPos)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    else if (pieceSelection.moveOptions.Contains((i, j)))
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }

                    Console.Write("[");

                    var piece = grid[i, j];
                    if (piece.color == PieceColor.Black)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }

                    if (piece.color == PieceColor.White)
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

        public void moveCursor(UserAction userAction)
        {
            cursor.Move(userAction);
        }

        public void handleEnter(PieceColor currentPlayer)
        {
            if (pieceSelection.piece == NullPiece.GetInstance())
            {
                selectCursorPiece(currentPlayer);
            }
            else if (IsValidMove(cursor.getCoordinates(), currentPlayer))
            {
                MoveSelectedPiece(cursor.getCoordinates());
            }
        }

        private void selectCursorPiece(PieceColor currentPlayer)
        {
            var coordinates = cursor.getCoordinates();
            var piece = grid[coordinates.Item1, coordinates.Item2];
            var isCurrentPlayerPiece = piece.color == currentPlayer;
            if (isCurrentPlayerPiece)
            {
                var moveOptions = piece.GetMoveOptions(this, coordinates);
                pieceSelection = new PieceSelection(coordinates, moveOptions, piece);
            }
        }

        private void MoveSelectedPiece((int, int) coordinates)
        {
            grid[coordinates.Item1, coordinates.Item2] = pieceSelection.piece;
            grid[
                pieceSelection.coordinates.Item1,
                pieceSelection.coordinates.Item2
            ] = NullPiece.GetInstance();

            var newCoordinates = (0, 0);
            var moveOptions = new HashSet<(int, int)> { };
            var piece = NullPiece.GetInstance();
            pieceSelection = new PieceSelection(newCoordinates, moveOptions, piece);
        }

        public bool IsValidMove((int, int) position, PieceColor color)
        {
            var hasXCoordinate = position.Item1 >= 0 && position.Item1 < GridLength;
            var hasYCoordinate = position.Item2 >= 0 && position.Item2 < GridLength;
            var isOnBoard = hasXCoordinate && hasYCoordinate;
            
            return isOnBoard && grid[position.Item1, position.Item2].color != color;
        }

        private Piece[,] CreateGrid()
        {
            var grid = new Piece[GridLength, GridLength];

            for (int i = 0; i < GridLength; i += 1)
            {
                for (int j = 0; j < GridLength; j += 1)
                {
                    grid[i, j] = NullPiece.GetInstance();
                }
            }

            for (int i = 0; i < Board.GridLength; i += 1)
            {
                grid[1, i] = new Pawn(PieceColor.Black);
                grid[6, i] = new Pawn(PieceColor.White);
            }

            grid[0, 0] = new Rook(PieceColor.Black);
            grid[0, GridLength - 1] = new Rook(PieceColor.Black);
            grid[GridLength - 1, 0] = new Rook(PieceColor.White);
            grid[GridLength - 1, GridLength - 1] = new Rook(PieceColor.White);

            grid[0, 1] = new Knight(PieceColor.Black);
            grid[0, GridLength - 2] = new Knight(PieceColor.Black);
            grid[GridLength - 1, 1] = new Knight(PieceColor.White);
            grid[GridLength - 1, GridLength - 2] = new Knight(PieceColor.White);

            grid[0, 2] = new Bishop(PieceColor.Black);
            grid[0, GridLength - 3] = new Bishop(PieceColor.Black);
            grid[GridLength - 1, 2] = new Bishop(PieceColor.White);
            grid[GridLength - 1, GridLength - 3] = new Bishop(PieceColor.White);

            grid[0, 3] = new Queen(PieceColor.Black);
            grid[GridLength - 1, 3] = new Queen(PieceColor.White);

            grid[0, GridLength - 4] = new King(PieceColor.Black);
            grid[GridLength - 1, GridLength - 4] = new King(PieceColor.White);

            return grid;
        }
    }
}

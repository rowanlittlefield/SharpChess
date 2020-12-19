using System;

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

        public bool MoveCursor(UserAction userAction)
        {
            cursor.Move(userAction);
            return false;
        }

        public bool SelectCursorPosition(PieceColor currentPlayer)
        {
            if (pieceSelection.moveOptions.Contains(cursor.getCoordinates()))
            {
                MoveSelectedPiece(cursor.getCoordinates());
                return true;
            }
            
            selectCursorPiece(currentPlayer);
            return false;
        }

        private void selectCursorPiece(PieceColor currentPlayer)
        {
            var coordinates = cursor.getCoordinates();
            var piece = grid[coordinates.Item1, coordinates.Item2];
            var isCurrentPlayerPiece = piece.color == currentPlayer;
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

        private void MoveSelectedPiece((int, int) coordinates)
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

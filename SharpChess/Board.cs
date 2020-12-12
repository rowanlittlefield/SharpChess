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
                    else if (pieceSelection.coordinates == pos)
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
                var moveOptions = piece.GetMoveOptions(this, coordinates);
                pieceSelection = new PieceSelection(coordinates, moveOptions, piece);
            }
            else
            {
                pieceSelection = NullPieceSelection.GetInstance();
            }
        }

        private void MoveSelectedPiece((int, int) coordinates)
        {
            grid[coordinates.Item1, coordinates.Item2] = pieceSelection.piece;
            grid[
                pieceSelection.coordinates.Item1,
                pieceSelection.coordinates.Item2
            ] = NullPiece.GetInstance();

            pieceSelection = NullPieceSelection.GetInstance();
        }

        public bool IsValidMove((int, int) position, PieceColor color)
        {
            var hasXCoordinate = position.Item1 >= 0 && position.Item1 < GridLength;
            var hasYCoordinate = position.Item2 >= 0 && position.Item2 < GridLength;
            var isOnBoard = hasXCoordinate && hasYCoordinate;
            
            return isOnBoard && grid[position.Item1, position.Item2].color != color;
        }
    }
}

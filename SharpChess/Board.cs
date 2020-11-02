using System;
namespace SharpChess
{
    public class Board
    {
        private static int GridLength = 8;
        private Cursor cursor;
        private Piece[,] grid;

        public Board()
        {
            cursor = new Cursor(Board.GridLength);
            grid = CreateGrid();
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
                    var isCursorPos = cursorPos[0] == i && cursorPos[1] == j;
                    if (isCursorPos)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
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
                grid[1, i] = new Pawn(PieceColor.White);
                grid[6, i] = new Pawn(PieceColor.Black);
            }

            grid[0, 0] = new Rook(PieceColor.White);
            grid[0, GridLength - 1] = new Rook(PieceColor.White);
            grid[GridLength - 1, 0] = new Rook(PieceColor.Black);
            grid[GridLength - 1, GridLength - 1] = new Rook(PieceColor.Black);

            grid[0, 1] = new Knight(PieceColor.White);
            grid[0, GridLength - 2] = new Knight(PieceColor.White);
            grid[GridLength - 1, 1] = new Knight(PieceColor.Black);
            grid[GridLength - 1, GridLength - 2] = new Knight(PieceColor.Black);

            grid[0, 2] = new Bishop(PieceColor.White);
            grid[0, GridLength - 3] = new Bishop(PieceColor.White);
            grid[GridLength - 1, 2] = new Bishop(PieceColor.Black);
            grid[GridLength - 1, GridLength - 3] = new Bishop(PieceColor.Black);


            return grid;
        }
    }
}

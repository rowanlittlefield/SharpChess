using System;
namespace SharpChess
{
    public class GridBuilder
    {
        public static readonly int GridLength = 8;
        public GridBuilder()
        {
        }

        public Piece[,] CreateGrid()
        {
            var grid = new Piece[GridLength, GridLength];
            var path = "/Users/rowanlittlefield/Projects/SharpChess/SharpChess/default-board.txt";
            var gridTextLines = System.IO.File.ReadAllLines(path);

            var row = 0;
            foreach (var line in gridTextLines)
            {
                var col = 0;
                var linePieceStrings = line.Split(",");
                foreach(var pieceString in linePieceStrings)
                {
                    if (!string.IsNullOrEmpty(pieceString))
                    {
                        var piece = GetPiece(pieceString);
                        grid[row, col] = piece;
                        col += 1;
                    }
                }
                row += 1;
            }


            return grid;
        }

        private Piece GetPiece(string pieceString)
        {
            var tokens = pieceString.Split(":");
            var pieceClassString = tokens[0];
            var pieceColorString = tokens[1];

            PieceColor color;

            switch (pieceColorString)
            {
                case "w":
                    color = PieceColor.White;
                    break;
                case "b":
                    color = PieceColor.Black;
                    break;
                default:
                    return NullPiece.GetInstance();
            }

            switch (pieceClassString)
            {
                case "p":
                    return new Pawn(color);
                case "r":
                    return new Rook(color);
                case "k":
                    return new Knight(color);
                case "b":
                    return new Bishop(color);
                case "Q":
                    return new Queen(color);
                case "K":
                    return new King(color);
                default:
                    return NullPiece.GetInstance();
            }
        }
    }
}

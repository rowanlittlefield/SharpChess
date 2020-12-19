using System;
namespace SharpChess
{
    public class GridBuilder
    {
        public static readonly int GridLength = SharedConstants.GridLength;
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
                var pieceTokens = line.Split(",");
                foreach(var pieceToken in pieceTokens)
                {
                    if (!string.IsNullOrEmpty(pieceToken))
                    {
                        var piece = ParseToken(pieceToken, (row, col));
                        grid[row, col] = piece;
                        col += 1;
                    }
                }
                row += 1;
            }


            return grid;
        }

        private Piece ParseToken(string pieceToken, (int, int) coordinates)
        {
            var tokens = pieceToken.Split(":");
            var pieceClassToken = tokens[0];
            var pieceColorToken = tokens[1];

            PieceColor color;
            switch (pieceColorToken)
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

            switch (pieceClassToken)
            {
                case "p":
                    return new Pawn(color, coordinates);
                case "r":
                    return new Rook(color, coordinates);
                case "k":
                    return new Knight(color, coordinates);
                case "b":
                    return new Bishop(color, coordinates);
                case "Q":
                    return new Queen(color, coordinates);
                case "K":
                    return new King(color, coordinates);
                default:
                    return NullPiece.GetInstance();
            }
        }
    }
}

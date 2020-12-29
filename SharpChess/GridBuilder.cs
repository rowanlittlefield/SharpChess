namespace SharpChess
{
    public static class GridBuilder
    {
        public static readonly int GridLength = SharedConstants.GridLength;

        public static Piece[,] CreateGrid()
        {
            var grid = new Piece[GridLength, GridLength];
            var gridTextLines = FileHandler.GetDefaultBoard();

            var row = 0;
            foreach (var line in gridTextLines)
            {
                var col = 0;
                var pieceTokens = line.Split(",");
                foreach(var pieceToken in pieceTokens)
                {
                    if (!string.IsNullOrEmpty(pieceToken))
                    {
                        var piece = _parseToken(pieceToken, (row, col));
                        grid[row, col] = piece;
                        col += 1;
                    }
                }
                row += 1;
            }


            return grid;
        }

        private static Piece _parseToken(string pieceToken, (int, int) coordinates)
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

        public static Piece[,] CloneGrid(Piece[,] grid)
        {
            var gridDup = new Piece[GridLength, GridLength];
            for (int row = 0; row < GridLength; row++)
            {
                for (int col = 0; col < GridLength; col++)
                {
                    var piece = grid[row, col];
                    gridDup[row, col] = piece.Clone();
                }
            }

            return gridDup;
        }

        public static string[] ToTokens(Board board)
        {
            var tokenizer = new PieceTokenizer();
            var lines = new string[8];
            for (int row = 0; row < GridLength; row++)
            {
                for (int col = 0; col < GridLength; col++)
                {
                    var piece = board.GetPiece((row, col));
                    piece.Accept(tokenizer);
                }
                lines[row] = $"{tokenizer.Tokens}";
                tokenizer.ClearTokens();
            }

            return lines;
        }
    }
}

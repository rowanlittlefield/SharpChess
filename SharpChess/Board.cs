using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpChess
{
    public class Board
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private Cursor _cursor;
        private Piece[,] _grid;
        private PieceSelection _pieceSelection;

        public Board()
        {
            _cursor = new Cursor(Board.GridLength);
            _grid = new GridBuilder().CreateGrid();
            _pieceSelection = NullPieceSelection.GetInstance();
        }

        private Board(Piece[,] grid)
        {
            _cursor = new Cursor(Board.GridLength);
            _grid = _dupGrid(grid);
            _pieceSelection = NullPieceSelection.GetInstance();
        }

        private Piece[,] _dupGrid(Piece[,] grid)
        {
            var gridDup = new Piece[GridLength,GridLength];
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

        public bool GameOver(PieceColor playerColor)
        {
            return _isInCheckmate(playerColor);
        }

        private bool _isInCheckmate(PieceColor playerColor)
        {
            var isInCheckmate = true;
            var playerPieces = _gridToQuery()
                .Where(piece => piece.Color == playerColor);

            foreach (var piece in playerPieces)
            {
                var hasValidMoves = _filterValidMoves(piece).Count() > 0;
                if (hasValidMoves)
                {
                    isInCheckmate = false;
                    break;
                }
            }

            return isInCheckmate;
        }

        private HashSet<(int, int)> _filterValidMoves(Piece piece)
        {
            var validMoveOptions = new HashSet<(int, int)> { };

            foreach (var move in piece.GetMoveOptions(this))
            {
                var boardDup = new Board(_grid);
                var pieceDup = boardDup.GetPiece(piece.Coordinates);
                boardDup._movePiece(pieceDup, move);
                if (!boardDup.IsInCheck(piece.Color))
                {
                    validMoveOptions.Add(move);
                }
            }

            return validMoveOptions;
        }

        public bool IsInCheck(PieceColor playerColor)
        {
            var kingPosition = (0, 0);
            foreach (var piece in _grid)
            {
                var isPlayersKing = piece is King && piece.Color == playerColor;
                if (isPlayersKing)
                {
                    kingPosition = piece.Coordinates;
                }
            }

            return _gridToQuery()
                .Where(piece => piece.Color == playerColor.GetOpposingColor())
                .Any(piece => piece.GetMoveOptions(this).Contains(kingPosition));
        }

        public void Render()
        {
            var cursorPos = _cursor.getCoordinates();
            for (int i = 0; i < Board.GridLength; i += 1)
            {
                for(int j = 0; j < Board.GridLength; j += 1)
                {
                    var pos = (i, j);
                    if (cursorPos == pos)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    else if (_pieceSelection.moveOptions.Contains(pos))
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else if (_pieceSelection.piece.Coordinates == pos)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    Console.Write("[");

                    var piece = _grid[i, j];
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
            _cursor.Move(userAction);
            return false;
        }

        public bool SelectCursorPosition(PieceColor currentPlayer)
        {
            if (_pieceSelection.moveOptions.Contains(_cursor.getCoordinates()))
            {
                _moveSelectedPiece(_cursor.getCoordinates());
                return true;
            }
            
            _selectCursorPiece(currentPlayer);
            return false;
        }

        private void _selectCursorPiece(PieceColor currentPlayer)
        {
            var coordinates = _cursor.getCoordinates();
            var piece = _grid[coordinates.Item1, coordinates.Item2];
            var isCurrentPlayerPiece = piece.Color == currentPlayer;
            if (isCurrentPlayerPiece)
            {
                var validMoveOptions = _filterValidMoves(piece);
                _pieceSelection = new PieceSelection(piece, validMoveOptions);
            }
            else
            {
                _pieceSelection = NullPieceSelection.GetInstance();
            }
        }

        private void _moveSelectedPiece((int, int) coordinates)
        {
            _movePiece(_pieceSelection.piece, coordinates);
            _pieceSelection = NullPieceSelection.GetInstance();
        }

        private void _movePiece(Piece piece, (int, int) coordinates)
        {
            var (oldRow, oldCol) = piece.Coordinates;
            var (row, col) = coordinates;
            piece.Move(coordinates);
            _grid[row, col] = piece;
            _grid[oldRow, oldCol] = NullPiece.GetInstance();
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
            return _grid[row, col];
        }

        private IEnumerable<Piece> _gridToQuery()
        {
            return from Piece piece in _grid select piece;
        }
    }
}

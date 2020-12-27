using System.Collections.Generic;
using System.Linq;

namespace SharpChess
{
    public class Board
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private Cursor _cursor;
        private Piece[,] _grid;
        public PieceSelection PieceSelection { get; private set; }
        private BoardView _view;
        public bool IsFlipped { get; private set; }

        public Board()
        {
            _cursor = new Cursor(Board.GridLength);
            _grid = GridBuilder.CreateGrid();
            PieceSelection = NullPieceSelection.GetInstance();
            _view = new BoardView();
            IsFlipped = false;
        }

        private Board(Piece[,] grid)
        {
            _cursor = new Cursor(Board.GridLength);
            _grid = GridBuilder.CloneGrid(grid);
            PieceSelection = NullPieceSelection.GetInstance();
            _view = new BoardView();
            IsFlipped = false;
        }

        public bool GameOver(PieceColor playerColor)
        {
            return _isInCheckmate(playerColor);
        }

        public PieceColor GetWinner()
        {
            if (_isInCheckmate(PieceColor.Black))
            {
                return PieceColor.White;
            }

            if (_isInCheckmate(PieceColor.White))
            {
                return PieceColor.Black;
            }

            return PieceColor.Null;
        }

        public bool _isInCheckmate(PieceColor playerColor)
        {
            return _gridToQuery()
                .Where(piece => piece.Color == playerColor)
                .All(piece => _filterValidMoves(piece).Count() == 0);
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
            _view.Render(this);
        }

        public BoardMemento ToggleTheme()
        {
            _view.ToggleTheme();
            return NullBoardMemento.GetInstance();
        }

        public BoardMemento FlipBoard()
        {
            IsFlipped = !IsFlipped;
            return NullBoardMemento.GetInstance();
        }

        public (int, int) GetCursorCoordinates()
        {
            return _cursor.getCoordinates();
        }

        public BoardMemento MoveCursor(UserAction userAction)
        {
            var direction = IsFlipped ? userAction.FlipVertically() : userAction;
            _cursor.Move(direction);
            return NullBoardMemento.GetInstance();
        }

        public BoardMemento SelectCursorPosition(PieceColor currentPlayer)
        {
            if (PieceSelection.moveOptions.Contains(_cursor.getCoordinates()))
            {
                var start = PieceSelection.piece.Coordinates;
                var end = _cursor.getCoordinates();
                var piece = PieceSelection.piece;
                _moveSelectedPiece(_cursor.getCoordinates());
                return new MovePieceBoardMemento(piece, start, end);
            }
            
            _selectCursorPiece(currentPlayer);
            return NullBoardMemento.GetInstance();
        }

        private void _selectCursorPiece(PieceColor currentPlayer)
        {
            var coordinates = _cursor.getCoordinates();
            var piece = _grid[coordinates.Item1, coordinates.Item2];
            var isCurrentPlayerPiece = piece.Color == currentPlayer;
            if (isCurrentPlayerPiece)
            {
                var validMoveOptions = _filterValidMoves(piece);
                PieceSelection = new PieceSelection(piece, validMoveOptions);
            }
            else
            {
                PieceSelection = NullPieceSelection.GetInstance();
            }
        }

        private void _moveSelectedPiece((int, int) coordinates)
        {
            _movePiece(PieceSelection.piece, coordinates);
            PieceSelection = NullPieceSelection.GetInstance();
        }

        private void _movePiece(Piece piece, (int, int) coordinates)
        {
            var (oldRow, oldCol) = piece.Coordinates;
            var (row, col) = coordinates;
            piece.Move(coordinates);
            _grid[row, col] = piece;
            _grid[oldRow, oldCol] = NullPiece.GetInstance();
        }

        private void _undoMovePiece(Piece piece, (int, int) coordinates)
        {
            var (oldRow, oldCol) = piece.Coordinates;
            var (row, col) = coordinates;
            piece.UndoMove(coordinates);
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

        public BoardMemento RevertSetSpace(MovePieceBoardMemento memento)
        {
            _cursor.SetCoordinates(memento.EndCoordinates);
            _undoMovePiece(memento.Piece, memento.StartCoordinates);
            PieceSelection = NullPieceSelection.GetInstance();
            return new UndoMoveBoardMemento();
        }

        public BoardMemento RedoSetSpace(MovePieceBoardMemento memento)
        {
            _cursor.SetCoordinates(memento.EndCoordinates);
            _movePiece(memento.Piece, memento.EndCoordinates);
            PieceSelection = NullPieceSelection.GetInstance();
            return new RedoMoveBoardMemento();
        }
    }
}

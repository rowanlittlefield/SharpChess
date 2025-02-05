﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpChess
{
    public class EndTurnEventArgs : EventArgs
    {
        public BoardMemento Memento { get; private set; }

        public EndTurnEventArgs(BoardMemento memento)
        {
            Memento = memento;
        }
    }

    public class Board
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private Cursor _cursor;
        private Piece[,] _grid;
        public PieceSelection PieceSelection { get; private set; }
        private BoardView _view;
        public bool IsFlipped { get; private set; }

        public Board(string[] gridLines)
        {
            _cursor = new Cursor(Board.GridLength);
            _grid = GridBuilder.CreateGrid(gridLines);
            PieceSelection = NullPieceSelection.GetInstance();
            _view = new BoardView(this);
            IsFlipped = false;
        }

        private Board(Piece[,] grid)
        {
            _cursor = new Cursor(Board.GridLength);
            _grid = GridBuilder.CloneGrid(grid);
            PieceSelection = NullPieceSelection.GetInstance();
            _view = new BoardView(this);
            IsFlipped = false;
        }

        public View GetView()
        {
            return _view;
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

        public void ToggleTheme()
        {
            _view.ToggleTheme();
        }

        public void FlipBoard()
        {
            IsFlipped = !IsFlipped;
        }

        public (int, int) GetCursorCoordinates()
        {
            return _cursor.getCoordinates();
        }

        public void MoveCursor(UserAction userAction)
        {
            var direction = IsFlipped ? userAction.FlipVertically() : userAction;
            _cursor.Move(direction);
        }

        public void SelectCursorPosition(PieceColor currentPlayer)
        {
            if (PieceSelection.moveOptions.Contains(_cursor.getCoordinates()))
            {
                var start = PieceSelection.piece.Coordinates;
                var end = _cursor.getCoordinates();
                var piece = PieceSelection.piece;
                _moveSelectedPiece(_cursor.getCoordinates());
                var memento = new BoardMemento(piece, start, end);
                var payload = new EndTurnEventArgs(memento);
                OnEndTurn(payload);
            } else
            {
                _selectCursorPiece(currentPlayer);
            }
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

        public void RevertSetSpace(BoardMemento memento)
        {
            _cursor.SetCoordinates(memento.EndCoordinates);
            _movePiece(memento.Piece, memento.StartCoordinates);
            PieceSelection = NullPieceSelection.GetInstance();
        }

        public void RedoSetSpace(BoardMemento memento)
        {
            _cursor.SetCoordinates(memento.EndCoordinates);
            _movePiece(memento.Piece, memento.EndCoordinates);
            PieceSelection = NullPieceSelection.GetInstance();
        }

        public event EventHandler<EndTurnEventArgs> EndTurn;

        protected virtual void OnEndTurn(EndTurnEventArgs args)
        {
            if (EndTurn != null)
            {
                EndTurn(this, args);
            }
        }
    }
}

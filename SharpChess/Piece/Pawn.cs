using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class Pawn : Piece
    {
        private static readonly Dictionary<PieceColor, int> _columnDirectionMap = new Dictionary<PieceColor, int>()
        {
            { PieceColor.Black, 1 },
            { PieceColor.White, -1 },
        };
        private readonly int _columnDirection;
        private bool _hasMoved;
        public Pawn(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
            _columnDirection = Pawn._columnDirectionMap.GetValueOrDefault(Color);
            _hasMoved = false;
        }

        public override string Render()
        {
            return "p";
        }

        public override void Move((int, int) coordinates)
        {
            base.Move(coordinates);
            _hasMoved = true;
        }

        public override Piece Clone()
        {
            return new Pawn(Color, Coordinates);
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            var (row, col) = Coordinates;
            var moveOptions = new HashSet<(int, int)> {};

            if (_canPerformDoubleMove(board))
            {
                var doubleMove = (row + (2 * _columnDirection), col);
                moveOptions.Add(doubleMove);
            }

            if (_canPerformSingleMove(board))
            {
                var singleMove = (row + _columnDirection, col);
                moveOptions.Add(singleMove);
            }

            if (_canPerformLeftDiagonalMove(board))
            {
                var leftDiagonalMove = (row + _columnDirection, col - 1);
                moveOptions.Add(leftDiagonalMove);
            }

            if (_canPerformRightDiagonalMove(board))
            {
                var rightDiagonalMove = (row + _columnDirection, col + 1);
                moveOptions.Add(rightDiagonalMove);
            }

            return moveOptions;
        }

        private bool _canPerformDoubleMove(Board board)
        {
            var (row, col) = Coordinates;
            var doubleMove = (row + (2 * _columnDirection), col);
            if (!board.IsOnBoard(doubleMove))
            {
                return false;
            }

            var pieceInFront = board.GetPiece((row + _columnDirection, col));
            var pieceTwoAway = board.GetPiece((row + (2 * _columnDirection), col));
            return pieceInFront.IsNullPiece()
                && pieceTwoAway.IsNullPiece()
                && !_hasMoved;
        }

        private bool _canPerformSingleMove(Board board)
        {
            var (row, col) = Coordinates;
            var singleMove = (row + _columnDirection, col);
            if (!board.IsOnBoard(singleMove))
            {
                return false;
            }

            var pieceInFront = board.GetPiece((row + _columnDirection, col));
            return pieceInFront.IsNullPiece();
        }

        private bool _canPerformLeftDiagonalMove(Board board)
        {
            var (row, col) = Coordinates;
            var leftDiagonalMove = (row + _columnDirection, col - 1);
            if (!board.IsOnBoard(leftDiagonalMove))
            {
                return false;
            }

            var leftDiagonalPiece = board.GetPiece(leftDiagonalMove);
            return !leftDiagonalPiece.IsNullPiece() && leftDiagonalPiece.Color != Color;
        }

        private bool _canPerformRightDiagonalMove(Board board)
        {
            var (row, col) = Coordinates;
            var rightDiagonalMove = (row + _columnDirection, col + 1);
            if (!board.IsOnBoard(rightDiagonalMove))
            {
                return false;
            }

            var rightDiagonalPiece = board.GetPiece((row + _columnDirection, col + 1));
            return !rightDiagonalPiece.IsNullPiece() && rightDiagonalPiece.Color != Color;
        }
    }
}

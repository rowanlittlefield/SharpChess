using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class Pawn : Piece
    {
        private bool _hasMoved;
        public Pawn(PieceColor color, (int, int) coordinates) : base(color, coordinates)
        {
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

        public override HashSet<(int, int)> GetMoveOptions(Board board)
        {
            var (row, col) = Coordinates;
            var moveOptions = new HashSet<(int, int)> {};
            int columnDirection;
            switch (this.color)
            {
                case PieceColor.Black:
                    columnDirection = 1;
                    break;
                case PieceColor.White:
                    columnDirection = -1;
                    break;
                default:
                    throw new Exception("Invalid value for this.color");
            }

            if (_canPerformDoubleMove(board, columnDirection))
            {
                var doubleMove = (row + (2 * columnDirection), col);
                moveOptions.Add(doubleMove);
            }

            if (_canPerformSingleMove(board, columnDirection))
            {
                var singleMove = (row + columnDirection, col);
                moveOptions.Add(singleMove);
            }

            if (_canPerformLeftDiagonalMove(board, columnDirection))
            {
                var leftDiagonalMove = (row + columnDirection, col - 1);
                moveOptions.Add(leftDiagonalMove);
            }

            if (_canPerformRightDiagonalMove(board, columnDirection))
            {
                var rightDiagonalMove = (row + columnDirection, col + 1);
                moveOptions.Add(rightDiagonalMove);
            }

            return moveOptions;
        }

        private bool _canPerformDoubleMove(Board board, int columnDirection)
        {
            var (row, col) = Coordinates;
            var doubleMove = (row + (2 * columnDirection), col);
            if (!board.IsOnBoard(doubleMove))
            {
                return false;
            }

            var pieceInFront = board.GetPiece((row + columnDirection, col));
            var pieceTwoAway = board.GetPiece((row + (2 * columnDirection), col));
            return pieceInFront.IsNullPiece()
                && pieceTwoAway.IsNullPiece()
                && !_hasMoved;
        }

        private bool _canPerformSingleMove(Board board, int columnDirection)
        {
            var (row, col) = Coordinates;
            var singleMove = (row + columnDirection, col);
            if (!board.IsOnBoard(singleMove))
            {
                return false;
            }

            var pieceInFront = board.GetPiece((row + columnDirection, col));
            return pieceInFront.IsNullPiece();
        }

        private bool _canPerformLeftDiagonalMove(Board board, int columnDirection)
        {
            var (row, col) = Coordinates;
            var leftDiagonalMove = (row + columnDirection, col - 1);

            if (!board.IsValidMove(leftDiagonalMove, this.color))
            {
                return false;
            }

            var leftDiagonalPiece = board.GetPiece((row + columnDirection, col - 1));
            return !leftDiagonalPiece.IsNullPiece();
        }

        private bool _canPerformRightDiagonalMove(Board board, int columnDirection)
        {
            var (row, col) = Coordinates;
            var rightDiagonalMove = (row + columnDirection, col + 1);

            if (!board.IsValidMove(rightDiagonalMove, this.color))
            {
                return false;
            }

            var rightDiagonalPiece = board.GetPiece((row + columnDirection, col + 1));
            return !rightDiagonalPiece.IsNullPiece();
        }
    }
}

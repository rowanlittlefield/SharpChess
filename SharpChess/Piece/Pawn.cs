﻿using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class Pawn : Piece
    {
        private bool _HasMoved;
        public Pawn(PieceColor color) : base(color)
        {
            _HasMoved = false;
        }

        public override string Render()
        {
            return "p";
        }

        public override void Move()
        {
            _HasMoved = true;
        }

        public override HashSet<(int, int)> GetMoveOptions(Board board, (int, int) coordinates)
        {
            var (row, col) = coordinates;
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

            if (_canPerformDoubleMove(board, columnDirection, coordinates))
            {
                var doubleMove = (row + (2 * columnDirection), col);
                moveOptions.Add(doubleMove);
            }

            if (_canPerformSingleMove(board, columnDirection, coordinates))
            {
                var singleMove = (row + columnDirection, col);
                moveOptions.Add(singleMove);
            }

            if (_canPerformLeftDiagonalMove(board, columnDirection, coordinates))
            {
                var leftDiagonalMove = (row + columnDirection, col - 1);
                moveOptions.Add(leftDiagonalMove);
            }

            if (_canPerformRightDiagonalMove(board, columnDirection, coordinates))
            {
                var rightDiagonalMove = (row + columnDirection, col + 1);
                moveOptions.Add(rightDiagonalMove);
            }

            return moveOptions;
        }

        private bool _canPerformDoubleMove(Board board, int columnDirection, (int, int) coordinates)
        {
            var (row, col) = coordinates;
            var doubleMove = (row + (2 * columnDirection), col);
            if (!board.IsValidMove(doubleMove, this.color))
            {
                return false;
            }

            var pieceInFront = board.grid[row + columnDirection, col];
            var isPieceInFront = pieceInFront != NullPiece.GetInstance();
            var pieceTwoAway = board.grid[row + (2 * columnDirection), col];
            var isPieceTwoAway = pieceTwoAway != NullPiece.GetInstance();

            return !isPieceInFront && !isPieceTwoAway && !_HasMoved;
        }

        private bool _canPerformSingleMove(Board board, int columnDirection, (int, int) coordinates)
        {
            var (row, col) = coordinates;
            var singleMove = (row + columnDirection, col);
            if (!board.IsValidMove(singleMove, this.color))
            {
                return false;
            }

            var pieceInFront = board.grid[row + columnDirection, col];
            var isPieceInFront = pieceInFront != NullPiece.GetInstance();

            return !isPieceInFront;
        }

        private bool _canPerformLeftDiagonalMove(Board board, int columnDirection, (int, int) coordinates)
        {
            var (row, col) = coordinates;
            var leftDiagonalMove = (row + columnDirection, col - 1);

            if (!board.IsValidMove(leftDiagonalMove, this.color))
            {
                return false;
            }

            var leftDiagonalPiece = board.grid[row + columnDirection, col - 1];
            return leftDiagonalPiece != NullPiece.GetInstance();
        }

        private bool _canPerformRightDiagonalMove(Board board, int columnDirection, (int, int) coordinates)
        {
            var (row, col) = coordinates;
            var rightDiagonalMove = (row + columnDirection, col + 1);

            if (!board.IsValidMove(rightDiagonalMove, this.color))
            {
                return false;
            }

            var rightDiagonalPiece = board.grid[row + columnDirection, col + 1];
            return rightDiagonalPiece != NullPiece.GetInstance();
        }
    }
}

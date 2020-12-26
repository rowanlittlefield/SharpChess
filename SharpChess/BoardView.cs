﻿using System;
namespace SharpChess
{
    public class BoardView
    {
        public static readonly int GridLength = SharedConstants.GridLength;
        private PieceView[] _themes = new PieceView[]
        {
            new DefaultPieceView(),
            new PictoralPieceView(),
        };
        private int _pieceViewIndex;
        private PieceView _pieceView;

        public BoardView()
        {
            _pieceViewIndex = 0;
            _pieceView = _themes[_pieceViewIndex];
        }

        public void Render(Board board)
        {
            for (int row = 0; row < Board.GridLength; row += 1)
            {
                for (int col = 0; col < Board.GridLength; col += 1)
                {
                    var pos = (row, col);
                    _pieceView.Render(board, pos);
                }

                Console.WriteLine("");
            }
        }

        public void ToggleTheme()
        {
            _pieceViewIndex = (_pieceViewIndex + 1) % _themes.Length;
            _pieceView = _themes[_pieceViewIndex];
        }
    }
}
using System;

namespace SharpChess
{
    public class Game
    {
        private Board _board;
        private Controller _controller;
        private PieceColor _currentPlayer; 

        public Game()
        {
            _board = new Board();
            _controller = new Controller();
            _currentPlayer = PieceColor.White;
        }

        public void Play()
        {
            while (!_board.GameOver(_currentPlayer))
            {
                _playTurn();
            }

            _showGameOverMessage();
        }

        private void _playTurn()
        {
            var isTurnOver = false;
            while (!isTurnOver)
            {
                isTurnOver = _playTick();
            }

            _currentPlayer = _currentPlayer.GetOpposingColor();
        }

        private bool _playTick()
        {
            Console.Clear();
            _board.Render();
            Console.WriteLine("Current Player: {0}", _currentPlayer);
            if (_board.IsInCheck(_currentPlayer))
            {
                Console.WriteLine("In Check");
            }

            var userAction = _controller.getUserAction();

            switch (userAction)
            {
                case UserAction.Enter:
                    return _board.SelectCursorPosition(_currentPlayer);
                default:
                    return _board.MoveCursor(userAction);
            }
        }

        private void _showGameOverMessage()
        {
            Console.Clear();
            _board.Render();
            Console.WriteLine("Game Over!");
        }
    }
}

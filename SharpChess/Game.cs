using System;

namespace SharpChess
{
    public class Game
    {
        private Board _board;
        private Controller _controller;
        private PieceColor _currentPlayer;
        private History _history;

        public Game()
        {
            _board = new Board();
            _controller = new Controller();
            _currentPlayer = PieceColor.White;
            _history = new History();
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
            BoardMemento boardMemento = NullBoardMemento.GetInstance();
            while (!boardMemento.IsTurnOver)
            {
                boardMemento = _playTick();
            }

            if (boardMemento is MovePieceBoardMemento)
            {
                var movePieceMomento = (MovePieceBoardMemento)boardMemento;
                _history.Push(movePieceMomento);
            }

            _currentPlayer = _currentPlayer.GetOpposingColor();
        }

        private BoardMemento _playTick()
        {
            Console.Clear();
            _board.Render();
            Console.WriteLine("Turn: {0}", _history.NumberOfElapsedTurns() + 1);
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
                case UserAction.ToggleTheme:
                    return _board.ToggleTheme();
                case UserAction.FlipBoard:
                    return _board.FlipBoard();
                case UserAction.Undo:
                    return _history.Back(_board);
                case UserAction.Redo:
                    return _history.Forward(_board);
                default:
                    return _board.MoveCursor(userAction);
            }
        }

        private void _showGameOverMessage()
        {
            Console.Clear();
            _board.Render();
            Console.WriteLine("Game Over!");

            var winner = _board.GetWinner();
            switch (winner)
            {
                case PieceColor.Null:
                    Console.WriteLine("Draw!");
                    break;
                default:
                    Console.WriteLine("{0} wins!", winner);
                    break;
            }
        }
    }
}

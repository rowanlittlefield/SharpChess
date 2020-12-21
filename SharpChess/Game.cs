using System;

namespace SharpChess
{
    public class Game
    {
        private Board board;
        private Controller controller;
        private PieceColor currentPlayer; 

        public Game()
        {
            board = new Board();
            controller = new Controller();
            currentPlayer = PieceColor.White;
        }

        public void Play()
        {
            while (!board.GameOver())
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

            switch (currentPlayer)
            {
                case PieceColor.White:
                    currentPlayer = PieceColor.Black;
                    break;
                case PieceColor.Black:
                    currentPlayer = PieceColor.White;
                    break;
                default:
                    throw new Exception("Invalid player");
            }
        }

        private bool _playTick()
        {
            Console.Clear();
            board.Render();
            Console.WriteLine("Current Player: {0}", currentPlayer);

            var userAction = controller.getUserAction();

            switch (userAction)
            {
                case UserAction.Enter:
                    return board.SelectCursorPosition(currentPlayer);
                default:
                    return board.MoveCursor(userAction);
            }
        }

        private void _showGameOverMessage()
        {
            Console.Clear();
            board.Render();
            Console.WriteLine("Game Over!");
        }
    }
}

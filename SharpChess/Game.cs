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

            currentPlayer = currentPlayer.GetOpposingColor();
        }

        private bool _playTick()
        {
            Console.Clear();
            board.Render();
            Console.WriteLine("Current Player: {0}", currentPlayer);
            if (board.IsInCheck(currentPlayer))
            {
                Console.WriteLine("In Check");
            }

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

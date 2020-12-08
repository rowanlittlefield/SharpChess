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
                PlayTurn();
            }

            ShowGameOverMessage();
        }

        private void PlayTurn()
        {
            var isTurnOver = false;

            while (!isTurnOver)
            {
                isTurnOver = PlayTick();
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

        private bool PlayTick()
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

        private void ShowGameOverMessage()
        {
            Console.Clear();
            board.Render();
            Console.WriteLine("Game Over!");
        }
    }
}

using System;
namespace SharpChess
{
    public class Game
    {
        private Board board;
        private Controller controller;

        public Game()
        {
            board = new Board();
            controller = new Controller();
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
            Console.Clear();
            board.Render();
            Console.WriteLine("Play turn");

            var userAction = controller.getUserAction();
            board.moveCursor(userAction);
        }

        private void ShowGameOverMessage()
        {
            Console.Clear();
            board.Render();
            Console.WriteLine("Game Over!");
        }
    }
}

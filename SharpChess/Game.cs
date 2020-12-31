using System;

namespace SharpChess
{
    public class Game
    {
        private Controller _controller;
        private GameStack _gameStack;

        public Game()
        {
            _controller = new Controller();
            var mainMenu = new MainMenu();
            _gameStack = new GameStack(mainMenu);
        }

        public void Play()
        {
            while (_gameStack.StillRunning())
            {
                _playTick();
            }
        }

        private void _playTick()
        {
            Console.Clear();
            _gameStack.GetView().Render();
            var userAction = _controller.getUserAction();
            _gameStack.HandleUserAction(userAction);
        }
    }
}

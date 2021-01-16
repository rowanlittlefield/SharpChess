using System;

namespace SharpChess
{
    public class Game
    {
        private GameStack _gameStack;

        public Game()
        {
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
            var userAction = Controller.getUserAction();
            _gameStack.HandleUserAction(userAction);
        }
    }
}

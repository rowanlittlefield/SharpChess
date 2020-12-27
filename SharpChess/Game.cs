namespace SharpChess
{
    public class Game
    {
        private Controller _controller;
        private GameStack _gameStack;

        public Game()
        {
            _controller = new Controller();
            var match = new Match();
            _gameStack = new GameStack(match);
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
            _gameStack.Render();
            var userAction = _controller.getUserAction();
            _gameStack.HandleUserInput(userAction);
        }
    }
}

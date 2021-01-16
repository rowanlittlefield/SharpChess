using System.Collections.Generic;

namespace SharpChess
{
    public class GameStack : GameComponent
    {
        private List<GameComponent> _store;

        public GameStack(GameComponent initialElement)
        {
            _store = new List<GameComponent>() { initialElement };
        }

        public override View GetView()
        {
            return new GameStackView(_store);
        }

        public override Navigation HandleUserAction(UserAction userAction)
        {
            var element = _store[_store.Count - 1];
            var navigation = element.HandleUserAction(userAction);
            switch (navigation.Action)
            {
                case NavigationAction.Next:
                    _pop();
                    _push(navigation.GameComponent);
                    break;
                case NavigationAction.Push:
                    _push(navigation.GameComponent);
                    break;
                case NavigationAction.Close:
                    _pop();
                    break;
                default:
                    break;
            }

            return navigation;
        }

        private void _push(GameComponent element)
        {
            _store.Add(element);
        }

        private void _pop()
        {
            _store.RemoveAt(_store.Count - 1);
        }

        public bool StillRunning()
        {
            return _store.Count > 0;
        }
    }
}

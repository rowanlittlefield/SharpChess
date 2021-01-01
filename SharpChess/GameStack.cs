using System.Collections.Generic;

namespace SharpChess
{
    public class GameStack : GameElement
    {
        private List<GameElement> _store;

        public GameStack(GameElement initialElement)
        {
            _store = new List<GameElement>() { initialElement };
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
                    _push(navigation.GameElement);
                    break;
                case NavigationAction.Push:
                    _push(navigation.GameElement);
                    break;
                case NavigationAction.Close:
                    _pop();
                    break;
                default:
                    break;
            }

            return navigation;
        }

        private void _push(GameElement element)
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

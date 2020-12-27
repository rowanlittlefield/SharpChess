using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class GameStack
    {
        private List<GameElement> _store;

        public GameStack(GameElement initialElement)
        {
            _store = new List<GameElement>() { initialElement };
        }

        public bool StillRunning()
        {
            return _store.Count > 0;
        }

        public void Push(GameElement element)
        {
            _store.Add(element);
        }

        public void HandleUserInput(UserAction userAction)
        {
            var element = _store[_store.Count - 1];
            var isElementFinished = element.HandleUserAction(userAction);

            if (isElementFinished)
            {
                _store.RemoveAt(_store.Count - 1);
            }
        }

        public void Render()
        {
            Console.Clear();

            foreach (var element in _store)
            {
                var view = element.GetView();
                view.Render();
            }
        }
    }
}

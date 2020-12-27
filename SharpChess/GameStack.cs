using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class GameStack
    {
        private List<GameElement> _stack;

        public GameStack(GameElement initialElement)
        {
            _stack = new List<GameElement>() { initialElement };
        }

        public bool StillRunning()
        {
            return _stack.Count > 0;
        }

        public void Push(GameElement element)
        {
            _stack.Add(element);
        }

        public void HandleUserInput(UserAction userAction)
        {
            var element = _stack[_stack.Count - 1];
            var isElementFinished = element.HandleUserAction(userAction);

            if (isElementFinished)
            {
                _stack.RemoveAt(_stack.Count - 1);
            }
        }

        public void Render()
        {
            Console.Clear();

            foreach (var model in _stack)
            {
                var view = model.GetView();
                view.Render();
            }
        }
    }
}

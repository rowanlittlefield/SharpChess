using System;
namespace SharpChess
{
    public abstract class GameElement
    {
        public GameElement()
        {
        }

        public abstract bool HandleUserAction(UserAction userAction);

        public abstract View GetView();
    }
}

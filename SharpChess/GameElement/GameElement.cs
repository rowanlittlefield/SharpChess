namespace SharpChess
{
    public abstract class GameElement
    {
        public GameElement()
        {
        }

        public abstract Navigation HandleUserAction(UserAction userAction);

        public abstract View GetView();
    }
}

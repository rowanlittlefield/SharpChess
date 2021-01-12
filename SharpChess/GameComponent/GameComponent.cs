namespace SharpChess
{
    public abstract class GameComponent
    {
        public GameComponent()
        {
        }

        public abstract Navigation HandleUserAction(UserAction userAction);

        public abstract View GetView();
    }
}

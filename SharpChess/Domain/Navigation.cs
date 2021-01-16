namespace SharpChess
{
    public class Navigation
    {
        public readonly NavigationAction Action;
        public readonly GameComponent GameComponent;
        public Navigation(NavigationAction action, GameComponent gameElement)
        {
            Action = action;
            GameComponent = gameElement;
        }
    }
}

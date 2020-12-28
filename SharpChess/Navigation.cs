namespace SharpChess
{
    public class Navigation
    {
        public readonly NavigationAction Action;
        public readonly GameElement GameElement;
        public Navigation(NavigationAction action, GameElement gameElement)
        {
            Action = action;
            GameElement = gameElement;
        }
    }
}

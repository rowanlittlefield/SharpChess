namespace SharpChess
{
    public class MainMenu : GameElement
    {
        public readonly string[] Options = new string[]
        {
            "New Game",
        };
        public int OptionsIndex { get; private set;  }
        public MainMenu()
        {
            OptionsIndex = 0;
        }

        public override Navigation HandleUserAction(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Enter:
                    return new Navigation(NavigationAction.Next, new Match());
                default:
                    return new Navigation(NavigationAction.Null, NullGameElement.GetInstance());
            }

        }

        public override View GetView()
        {
            return new MainMenuView(this);
        }
    }
}

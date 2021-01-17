namespace SharpChess
{
    public class MainMenu : GameComponent
    {
        public readonly string[] Options;
        public int OptionsIndex { get; private set; }
        public MainMenu()
        {
            OptionsIndex = 0;

            Options = FileHandler.SavedMatchExists()
                ? new string[] { "New Game", "Load Game" }
                : new string[] { "New Game" };
        }

        public override Navigation HandleUserAction(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Enter:
                    return _handleEnter();
                case UserAction.Up:
                    OptionsIndex = OptionsIndex == 0 ? Options.Length - 1 : OptionsIndex - 1;
                    return new Navigation(NavigationAction.Null, NullGameComponent.GetInstance());
                case UserAction.Down:
                    OptionsIndex = (OptionsIndex + 1) % Options.Length;
                    return new Navigation(NavigationAction.Null, NullGameComponent.GetInstance());
                default:
                    return new Navigation(NavigationAction.Null, NullGameComponent.GetInstance());
            }
        }

        private Navigation _handleEnter()
        {
            Match match;
            switch (Options[OptionsIndex])
            {
                case "Load Game":
                    match = FileHandler.LoadSavedMatch();
                    break;
                default:
                    match = new Match();
                    break;
            }
            return new Navigation(NavigationAction.Next, match);
        }

        public override View GetView()
        {
            return new MainMenuView(this);
        }
    }
}

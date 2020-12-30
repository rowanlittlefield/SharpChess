using System;
namespace SharpChess
{
    public class MatchStartMenu : GameElement
    {
        public readonly string[] Options = new string[]
        {
            "Save",
            "Close",
        };
        public int OptionsIndex { get; private set; }
        private SaveMatchCommand _saveMatchCommand;

        public MatchStartMenu(SaveMatchCommand saveMatchCommand)
        {
            OptionsIndex = 0;
            _saveMatchCommand = saveMatchCommand;
        }

        public override View GetView()
        {
            return new MatchStartMenuView(this);
        }

        public override Navigation HandleUserAction(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Enter:
                    return _handleEnter();
                case UserAction.Up:
                    OptionsIndex = OptionsIndex == 0 ? Options.Length - 1 : OptionsIndex - 1;
                    return new Navigation(NavigationAction.Null, NullGameElement.GetInstance());
                case UserAction.Down:
                    OptionsIndex = (OptionsIndex + 1) % Options.Length;
                    return new Navigation(NavigationAction.Null, NullGameElement.GetInstance());
                default:
                    return new Navigation(NavigationAction.Null, NullGameElement.GetInstance());
            }
        }

        private Navigation _handleEnter()
        {
            var option = Options[OptionsIndex];
            switch (option)
            {
                case "Save":
                    _saveMatchCommand.Execute();
                    return new Navigation(NavigationAction.Close, NullGameElement.GetInstance());
                case "Close":
                    return new Navigation(NavigationAction.Close, NullGameElement.GetInstance());
                default:
                    return new Navigation(NavigationAction.Null, NullGameElement.GetInstance());
            }
        }
    }
}

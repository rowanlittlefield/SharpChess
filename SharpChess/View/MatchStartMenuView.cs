using System;
namespace SharpChess
{
    public class MatchStartMenuView : View
    {
        private MatchStartMenu _matchStartMenu;
        public MatchStartMenuView(MatchStartMenu matchStartMenu)
        {
            _matchStartMenu = matchStartMenu;
        }

        public override void Render()
        {
            Console.WriteLine("--------------");

            for (int index = 0; index < _matchStartMenu.Options.Length; index++)
            {
                var option = _matchStartMenu.Options[index];
                var cursor = _matchStartMenu.OptionsIndex == index ? " <" : "";
                Console.WriteLine("{0}{1}", option, cursor);
            }

            Console.WriteLine("--------------");
        }
    }
}

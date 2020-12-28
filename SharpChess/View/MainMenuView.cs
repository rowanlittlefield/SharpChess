using System;
namespace SharpChess
{
    public class MainMenuView : View
    {
        private MainMenu _mainMenu;
        public MainMenuView(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        public override void Render()
        {
            _renderBanner();

            for (int index  = 0; index < _mainMenu.Options.Length; index++)
            {
                var option = _mainMenu.Options[index];
                var cursor = _mainMenu.OptionsIndex == index ? " <" : "";
                Console.WriteLine("{0}{1}", option, cursor);
            }
        }

        private void _renderBanner()
        {
            Console.WriteLine("##################");
            Console.WriteLine("####SharpChess####");
            Console.WriteLine("##################");
            Console.WriteLine("");
        }
    }
}

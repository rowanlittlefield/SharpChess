using System;
namespace SharpChess
{
    public class Controller
    {
        public Controller()
        {
        }

        public UserAction getUserAction()
        {
            var userAction = UserAction.Null;

            while (userAction == UserAction.Null)
            {
                var userInput = Console.ReadKey().KeyChar;
                userAction = _mapInput(userInput);
            }

            return userAction;
        }

        private UserAction _mapInput(char userInput)
        {
            switch (userInput)
            {
                case 'w':
                    return UserAction.Up;
                case 'd':
                    return UserAction.Right;
                case 's':
                    return UserAction.Down;
                case 'a':
                    return UserAction.Left;
                case ' ':
                    return UserAction.Enter;
                case 't':
                    return UserAction.ToggleTheme;
                default:
                    return UserAction.Null;
            }
        }
    }
}

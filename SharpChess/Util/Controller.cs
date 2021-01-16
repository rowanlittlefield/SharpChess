using System;
namespace SharpChess
{
    static public class Controller
    {

        static public UserAction getUserAction()
        {
            var userAction = UserAction.Null;

            while (userAction == UserAction.Null)
            {
                var userInput = Console.ReadKey().KeyChar;
                userAction = _mapInput(userInput);
            }

            return userAction;
        }

        static private UserAction _mapInput(char userInput)
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
                case 'q':
                    return UserAction.Start;
                case 't':
                    return UserAction.ToggleTheme;
                case 'f':
                    return UserAction.FlipBoard;
                case 'u':
                    return UserAction.Undo;
                case 'r':
                    return UserAction.Redo;
                default:
                    return UserAction.Null;
            }
        }
    }
}

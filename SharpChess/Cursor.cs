using System;
namespace SharpChess
{
    public class Cursor
    {
        private (int, int) coordinates;
        private int boardDimensions;

        public Cursor(int boardDimensions)
        {
            this.boardDimensions = boardDimensions;
            coordinates = (0, 0);
        }

        public (int, int) getCoordinates()
        {
            return coordinates;
        }

        public void Move(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Up:
                    var y = coordinates.Item1;
                    coordinates.Item1 = y > 0 ? y - 1 : boardDimensions - 1;
                    break;
                case UserAction.Right:
                    coordinates.Item2 = (coordinates.Item2 + 1) % boardDimensions;
                    break;
                case UserAction.Down:
                    coordinates.Item1 = (coordinates.Item1 + 1) % boardDimensions;
                    break;
                case UserAction.Left:
                    var x = coordinates.Item2;
                    coordinates.Item2 = x > 0 ? x - 1 : boardDimensions - 1;
                    break;
                default:
                    break;
            }
        }
    }
}

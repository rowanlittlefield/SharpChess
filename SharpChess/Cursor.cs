using System;
namespace SharpChess
{
    public class Cursor
    {
        private int[] coordinates;
        private int boardDimensions;

        public Cursor(int boardDimensions)
        {
            this.boardDimensions = boardDimensions;
            coordinates = new int[2] { 0, 0 };
        }

        public int[] getCoordinates()
        {
            return coordinates;
        }

        public void Move(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Up:
                    var y = coordinates[0];
                    coordinates[0] = y > 0 ? y - 1 : boardDimensions - 1;
                    break;
                case UserAction.Right:
                    coordinates[1] = (coordinates[1] + 1) % boardDimensions;
                    break;
                case UserAction.Down:
                    coordinates[0] = (coordinates[0] + 1) % boardDimensions;
                    break;
                case UserAction.Left:
                    var x = coordinates[1];
                    coordinates[1] = x > 0 ? x - 1 : boardDimensions - 1;
                    break;
                default:
                    break;
            }
        }
    }
}

using System;
namespace SharpChess
{
    public class Cursor
    {
        private (int, int) _coordinates;
        private int _boardDimensions;

        public Cursor(int boardDimensions)
        {
            _boardDimensions = boardDimensions;
            _coordinates = (0, 0);
        }

        public (int, int) getCoordinates()
        {
            return _coordinates;
        }

        public void Move(UserAction userAction)
        {
            var (row, col) = _coordinates;
            switch (userAction)
            {
                case UserAction.Up:
                    _coordinates.Item1 = row > 0 ? row - 1 : _boardDimensions - 1;
                    break;
                case UserAction.Right:
                    _coordinates.Item2 = (col + 1) % _boardDimensions;
                    break;
                case UserAction.Down:
                    _coordinates.Item1 = (row + 1) % _boardDimensions;
                    break;
                case UserAction.Left:
                    _coordinates.Item2 = col > 0 ? col - 1 : _boardDimensions - 1;
                    break;
                default:
                    break;
            }
        }
    }
}

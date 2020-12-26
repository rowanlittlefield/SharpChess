using System;
namespace SharpChess
{
    public class MovePieceBoardMemento : BoardMemento
    {
        public MovePieceBoardMemento()
        {
            IsTurnOver = true;
        }
    }
}

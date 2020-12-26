using System;
namespace SharpChess
{
    public abstract class BoardMemento
    {
        public bool IsTurnOver { get; protected set; }

        public BoardMemento()
        {
        }
    }
}

using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class History
    {
        private List<BoardMemento> _pastMementos;

        public History()
        {
        }

        public int NumberOfElapsedTurns()
        {
            return _pastMementos.Count;
        }

        public void Push(BoardMemento memento)
        {
            _pastMementos.Add(memento);
        }
    }
}

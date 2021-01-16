using System;
using System.Collections.Generic;

namespace SharpChess
{
    public class History
    {
        private List<BoardMemento> _pastMementos;
        private List<BoardMemento> _futureMementos;

        public History()
        {
            _pastMementos = new List<BoardMemento>();
            _futureMementos = new List<BoardMemento>();
        }

        public int NumberOfElapsedTurns()
        {
            return _pastMementos.Count;
        }

        public void Push(BoardMemento memento)
        {
            _pastMementos.Add(memento);
            _futureMementos.Clear();
        }

        public void Back(Board board)
        {
            if (_pastMementos.Count > 0)
            {
                var lastItem = _pastMementos[_pastMementos.Count - 1];
                _pastMementos.RemoveAt(_pastMementos.Count - 1);
                board.RevertSetSpace(lastItem);
                _futureMementos.Add(lastItem);
                OnTimeTraveled();
            }
        }

        public void Forward(Board board)
        {
            if (_futureMementos.Count > 0)
            {
                var lastItem = _futureMementos[_futureMementos.Count - 1];
                _futureMementos.RemoveAt(_futureMementos.Count - 1);
                board.RedoSetSpace(lastItem);
                _pastMementos.Add(lastItem);
                OnTimeTraveled();
            }
        }

        public event EventHandler<EventArgs> TimeTraveled;

        protected virtual void OnTimeTraveled()
        {
            if (TimeTraveled != null)
            {
                TimeTraveled(this, EventArgs.Empty);
            }
        }
    }
}

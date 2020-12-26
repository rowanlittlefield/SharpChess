using System.Collections.Generic;

namespace SharpChess
{
    public class History
    {
        private List<MovePieceBoardMemento> _pastMementos;
        private List<MovePieceBoardMemento> _futureMementos;

        public History()
        {
            _pastMementos = new List<MovePieceBoardMemento>();
            _futureMementos = new List<MovePieceBoardMemento>();
        }

        public int NumberOfElapsedTurns()
        {
            return _pastMementos.Count;
        }

        public void Push(MovePieceBoardMemento memento)
        {
            _pastMementos.Add(memento);
            _futureMementos.Clear();
        }

        public BoardMemento Back(Board board)
        {
            if (_pastMementos.Count == 0)
            {
                return NullBoardMemento.GetInstance();
            }

            var lastItem = _pastMementos[_pastMementos.Count - 1];
            _pastMementos.RemoveAt(_pastMementos.Count - 1);
            board.RevertSetSpace(lastItem);
            _futureMementos.Add(lastItem);
            return new UndoMoveBoardMemento();
        }

        public BoardMemento Forward(Board board)
        {
            if (_futureMementos.Count == 0)
            {
                return NullBoardMemento.GetInstance();
            }

            var lastItem = _futureMementos[_futureMementos.Count - 1];
            _futureMementos.RemoveAt(_futureMementos.Count - 1);
            board.RedoSetSpace(lastItem);
            _pastMementos.Add(lastItem);
            return new UndoMoveBoardMemento();
        }
    }
}

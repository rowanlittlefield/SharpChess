namespace SharpChess
{
    public class UndoMoveBoardMemento : BoardMemento
    {
        public UndoMoveBoardMemento()
        {
            IsTurnOver = true;
        }
    }
}

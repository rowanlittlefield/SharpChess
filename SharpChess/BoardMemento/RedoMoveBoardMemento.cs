namespace SharpChess
{
    public class RedoMoveBoardMemento : BoardMemento
    {
        public RedoMoveBoardMemento()
        {
            IsTurnOver = true;
        }
    }
}

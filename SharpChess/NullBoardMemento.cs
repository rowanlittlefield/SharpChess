namespace SharpChess
{
    public sealed class NullBoardMemento : BoardMemento
    {
        private static NullBoardMemento _instance = null;

        private NullBoardMemento()
        {
            IsTurnOver = false;
        }

        public static NullBoardMemento GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NullBoardMemento();
            }

            return _instance;
        }
    }
}

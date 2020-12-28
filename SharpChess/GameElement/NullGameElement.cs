using System;
namespace SharpChess
{
    public sealed class NullGameElement : GameElement
    {
        static private NullGameElement _instance;
        private NullGameElement()
        {
        }

        public static NullGameElement GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NullGameElement();
            }

            return _instance;
        }

        public override Navigation HandleUserAction(UserAction userAction)
        {
            throw new NotImplementedException();
        }

        public override View GetView()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
namespace SharpChess
{
    public sealed class NullGameComponent : GameComponent
    {
        static private NullGameComponent _instance;
        private NullGameComponent()
        {
        }

        public static NullGameComponent GetInstance()
        {
            if (_instance == null)
            {
                _instance = new NullGameComponent();
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

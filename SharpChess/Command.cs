using System;
namespace SharpChess
{
    public abstract class Command
    {
        public Command()
        {
        }

        public abstract void Execute();
    }
}

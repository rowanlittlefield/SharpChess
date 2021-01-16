using System;
namespace SharpChess
{
    public class SaveMatchCommand : Command
    {
        private Match _match;
        public SaveMatchCommand(Match match)
        {
            _match = match;
        }

        public override void Execute()
        {
            _match.Save();
            //FileHandler.SaveMatch(_match);
            //Console.WriteLine("Game saved!");
            //System.Threading.Thread.Sleep(1000);
        }
    }
}

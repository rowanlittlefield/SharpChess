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
            Console.WriteLine("Saving...");
            System.Threading.Thread.Sleep(1000);

            var lines = _match.ToText();
            FileHandler.SaveMatch(lines);
        }
    }
}

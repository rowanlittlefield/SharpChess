namespace SharpChess
{
    public static class FileHandler
    {
        private static string _defaultMatchPath = "/Users/rowanlittlefield/Projects/SharpChess/SharpChess/SavedMatches/default-board.txt";
        private static string _savedMatchPath = "/Users/rowanlittlefield/Projects/SharpChess/SharpChess/SavedMatches/saved-board.txt";

        public static Match LoadDefaultMatch()
        {
            var matchTextLines = System.IO.File.ReadAllLines(_defaultMatchPath);
            return new Match(matchTextLines);
        }

        public static void SaveMatch(Match match)
        {
            var lines = match.ToText();
            System.IO.File.WriteAllLines(_savedMatchPath, lines);
        }

        public static Match LoadSavedMatch()
        {
            var matchTextLines = System.IO.File.ReadAllLines(_savedMatchPath);
            return new Match(matchTextLines);
        }
    }
}

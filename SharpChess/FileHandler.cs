namespace SharpChess
{
    public static class FileHandler
    {
        public static Match GetDefaultMatch()
        {
            var path = "/Users/rowanlittlefield/Projects/SharpChess/SharpChess/SavedMatches/default-board.txt";
            var matchTextLines = System.IO.File.ReadAllLines(path);
            return new Match(matchTextLines);
        }

        public static void SaveMatch(string[] lines)
        {
            var path = "/Users/rowanlittlefield/Projects/SharpChess/SharpChess/SavedMatches/saved-board.txt";
            System.IO.File.WriteAllLines(path, lines);
        }

        public static Match LoadSavedMatch()
        {
            var path = "/Users/rowanlittlefield/Projects/SharpChess/SharpChess/SavedMatches/saved-board.txt";
            var matchTextLines = System.IO.File.ReadAllLines(path);
            return new Match(matchTextLines);
        }
    }
}

using System;
using System.IO;

namespace SharpChess
{
    public static class FileHandler
    {
        public static void SaveMatch(Match match)
        {
            var destinationPath = _getSavedFilePath();
            var lines = match.ToText();
            System.IO.File.WriteAllLines(destinationPath, lines);
        }

        public static Match LoadSavedMatch()
        {
            var destinationPath = _getSavedFilePath();
            var matchTextLines = System.IO.File.ReadAllLines(destinationPath);
            return new Match(matchTextLines);
        }

        private static string _getSavedFilePath()
        {
            var additionalPathPart = "";
            #if DEBUG
                additionalPathPart = "../../../SavedMatches";
            #endif

            var parts = new string[] {
                Environment.CurrentDirectory,
                additionalPathPart,
                "saved-match.txt",
            };

            return Path.Combine(parts);
        }
    }
}

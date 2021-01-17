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

        public static bool SavedMatchExists()
        {
            var savedFilePath = _getSavedFilePath();
            return File.Exists(savedFilePath);
        }

        private static string _getSavedFilePath()
        {
            var debugRedirect = "";
            #if DEBUG
                debugRedirect = "../../../SavedMatches";
            #endif

            var parts = new string[] {
                Environment.CurrentDirectory,
                debugRedirect,
                "saved-match.txt",
            };

            return Path.Combine(parts);
        }
    }
}

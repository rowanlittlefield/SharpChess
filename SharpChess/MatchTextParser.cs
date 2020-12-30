using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SharpChess
{
    public class MatchTextParser
    {
        public readonly PieceColor CurrentPlayer;
        public readonly string[] GridTextLines;
        public readonly int NumberOfElapsedTurns;
        private string[] _matchTextFileLines;

        public MatchTextParser(string[] matchTextFileLines)
        {
            _matchTextFileLines = matchTextFileLines;
            GridTextLines = _getGridTextLines();
            NumberOfElapsedTurns = _getNumberOfElapsedTurns();
            CurrentPlayer = _getCurrentPlayer();

        }

        private string[] _getGridTextLines()
        {
            return _matchTextFileLines
                .Take(SharedConstants.GridLength)
                .ToArray();
        }

        private int _getNumberOfElapsedTurns()
        {
            var turnLine = _matchTextFileLines[SharedConstants.GridLength];
            var unrecordedTurnsMatch = Regex.Match(turnLine, @"^turn:([\d])+$");
            var unrecordedTurnsString = unrecordedTurnsMatch.Groups[1].Value;
            return Int32.Parse(unrecordedTurnsString);
        }

        private PieceColor _getCurrentPlayer()
        {
            return _getNumberOfElapsedTurns() % 2 == 0
                ? PieceColor.White
                : PieceColor.Black;
        }
    }
}

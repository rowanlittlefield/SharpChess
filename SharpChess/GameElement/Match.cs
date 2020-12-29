using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SharpChess
{
    public class Match : GameElement
    {
        private Board _board;
        private PieceColor _currentPlayer;
        private History _history;
        private int _unrecordedTurns;

        public Match()
        {
            _board = new Board();
            _currentPlayer = PieceColor.White;
            _history = new History();
            _unrecordedTurns = 0;

            _board.EndTurn += OnEndTurn;
            _history.TimeTraveled += OnTimeTraveled;
        }

        public Match(string[] lines)
        {
            var gridTextLines = lines.Take(8).ToArray();
            Console.WriteLine(lines[8]);
            System.Threading.Thread.Sleep(3000);
            var unrecordedTurnsMatch = Regex.Match(lines[8], " turn:(d)");
            Console.WriteLine(lines[8][lines[8].Length - 1]);
            System.Threading.Thread.Sleep(3000);
            //var unrecordedTurns = Int32.Parse(unrecordedTurnsMatch.Captures[0].Value);
            var unrecordedTurns = Int32.Parse(lines[8][lines[8].Length - 1].ToString());

            _board = new Board(gridTextLines);
            _currentPlayer = unrecordedTurns % 2 == 0 ? PieceColor.White : PieceColor.Black;
            _history = new History();
            _unrecordedTurns = unrecordedTurns;

            _board.EndTurn += OnEndTurn;
            _history.TimeTraveled += OnTimeTraveled;
        }

        public override Navigation HandleUserAction(UserAction userAction)
        {
            if (IsGameOver())
            {
                return new Navigation(NavigationAction.Next, new MainMenu());
            }

            return _playTick(userAction);

        }

        private Navigation _playTick(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Enter:
                    _board.SelectCursorPosition(_currentPlayer);
                    break;
                case UserAction.Start:
                    var command = new SaveMatchCommand(this);
                    return new Navigation(NavigationAction.Push, new MatchStartMenu(command));
                case UserAction.ToggleTheme:
                    _board.ToggleTheme();
                    break;
                case UserAction.FlipBoard:
                    _board.FlipBoard();
                    break;
                case UserAction.Undo:
                    _history.Back(_board);
                    break;
                case UserAction.Redo:
                    _history.Forward(_board);
                    break;
                default:
                    _board.MoveCursor(userAction);
                    break;
            }

            return new Navigation(NavigationAction.Null, NullGameElement.GetInstance());
        }

        public override View GetView()
        {
            return new MatchView(this);
        }

        public View GetBoardView()
        {
            return _board.GetView();
        }

        public bool IsGameOver()
        {
            return _board.GameOver(_currentPlayer);
        }

        public PieceColor GetWinner()
        {
            return _board.GetWinner();
        }

        public int TurnNumber()
        {
            return _unrecordedTurns + _history.NumberOfElapsedTurns() + 1;
        }

        public PieceColor GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public string[] ToText()
        {
            var allLines = new string[9];
            var pieceTokenLines = GridBuilder.ToTokens(_board);
            pieceTokenLines.CopyTo(allLines, 0);
            allLines[8] = $"turn:{TurnNumber() - 1}";
            return allLines;
        } 

        public void OnEndTurn(object source, EndTurnEventArgs e)
        {
            _history.Push(e.Memento);
            _currentPlayer = _currentPlayer.GetOpposingColor();
        }

        public void OnTimeTraveled(object source, EventArgs e)
        {
            _currentPlayer = _currentPlayer.GetOpposingColor();
        }
    }
}

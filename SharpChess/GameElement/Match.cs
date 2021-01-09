using System;

namespace SharpChess
{
    public class Match : GameElement
    {
        private Board _board;
        private PieceColor _currentPlayer;
        private History _history;
        private int _unrecordedTurns;
        private static string DEFAULT_MATCH_TEXT =
@"r:b,k:b,b:b,Q:b,K:b,b:b,k:b,r:b,
p:b,p:b,p:b,p:b,p:b,p:b,p:b,p:b,
 : , : , : , : , : , : , : , : ,
 : , : , : , : , : , : , : , : ,
 : , : , : , : , : , : , : , : ,
 : , : , : , : , : , : , : , : ,
p:w,p:w,p:w,p:w,p:w,p:w,p:w,p:w,
r:w,k:w,b:w,Q:w,K:w,b:w,k:w,r:w,
turn:0
";
        public Match()
        {
            var matchTextParser = new MatchTextParser(DEFAULT_MATCH_TEXT.Split('\n'));

            _board = new Board(matchTextParser.GridTextLines);
            _currentPlayer = matchTextParser.CurrentPlayer;
            _history = new History();
            _unrecordedTurns = matchTextParser.NumberOfElapsedTurns;

            _board.EndTurn += OnEndTurn;
            _history.TimeTraveled += OnTimeTraveled;
        }

        public Match(string[] matchTextFileLines)
        {
            var matchTextParser = new MatchTextParser(matchTextFileLines);

            _board = new Board(matchTextParser.GridTextLines);
            _currentPlayer = matchTextParser.CurrentPlayer;
            _history = new History();
            _unrecordedTurns = matchTextParser.NumberOfElapsedTurns;

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
            var allLines = new string[SharedConstants.GridLength + 1];
            var pieceTokenLines = GridBuilder.ToTokens(_board);
            pieceTokenLines.CopyTo(allLines, 0);
            allLines[SharedConstants.GridLength] = $"turn:{TurnNumber() - 1}";
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

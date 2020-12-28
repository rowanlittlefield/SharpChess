using System;

namespace SharpChess
{
    public class Match : GameElement
    {
        private Board _board;
        private PieceColor _currentPlayer;
        private History _history;

        public Match()
        {
            _board = new Board();
            _currentPlayer = PieceColor.White;
            _history = new History();

            _board.EndTurn += OnEndTurn;
            _history.TimeTraveled += OnTimeTraveled;
        }

        public override bool HandleUserAction(UserAction userAction)
        {
            if (IsGameOver())
            {
                return true;
            }

            _playTick(userAction);
            return false;
        }

        private void _playTick(UserAction userAction)
        {
            switch (userAction)
            {
                case UserAction.Enter:
                    _board.SelectCursorPosition(_currentPlayer);
                    break;
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
            return _history.NumberOfElapsedTurns() + 1;
        }

        public PieceColor GetCurrentPlayer()
        {
            return _currentPlayer;
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

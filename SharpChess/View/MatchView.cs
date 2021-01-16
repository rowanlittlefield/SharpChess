using System;
namespace SharpChess
{
    public class MatchView : View
    {
        private Match _match;

        public MatchView(Match match)
        {
            _match = match;
            _match.SavedGame += _renderGameSavedMessage;
        }

        public override void Render()
        {
            _match.GetBoardView().Render();
            if (_match.IsGameOver())
            {
                _renderGameOverMessage();
            }
            else
            {
                _renderTurnDetail();
            }
        }

        private void _renderGameOverMessage()
        {
            Console.WriteLine("Game over!");
            var winner = _match.GetWinner();
            switch (winner)
            {
                case PieceColor.Null:
                    Console.WriteLine("Draw!");
                    break;
                default:
                    Console.WriteLine("{0} wins!", winner);
                    break;
            }
        }

        private void _renderTurnDetail()
        {
            Console.WriteLine("Turn: {0}", _match.TurnNumber());
            Console.WriteLine("Current Player: {0}", _match.GetCurrentPlayer());
        }

        private void _renderGameSavedMessage(object source, EventArgs e)
        {
            Console.WriteLine("Game saved!");
            System.Threading.Thread.Sleep(1000);
        }
    }
}

using System.Collections.Generic;

namespace SharpChess
{
    public class GameStackView : View
    {
        private List<GameElement> _store;
        public GameStackView(List<GameElement> gameStackStore)
        {
            _store = gameStackStore;
        }

        public override void Render()
        {
            foreach (var element in _store)
            {
                var view = element.GetView();
                view.Render();
            }
        }
    }
}

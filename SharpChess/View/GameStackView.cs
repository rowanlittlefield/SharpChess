using System.Collections.Generic;

namespace SharpChess
{
    public class GameStackView : View
    {
        private List<GameComponent> _store;
        public GameStackView(List<GameComponent> gameStackStore)
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

using Events;

namespace ViewModels
{
    public class MainSceneVM
    {
        public bool InputEnabled{get;set;}

        public MainSceneVM()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            GridEvents.CardShowComplete += OnCloseCards;
        }

        private void OnCloseCards()
        {
            InputEnabled = true;
        }
    }
}
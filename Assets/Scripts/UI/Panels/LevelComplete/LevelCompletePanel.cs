using Events;
using Extensions.Unity.MonoHelper;

namespace UI.Panels.LevelComplete
{
    public class LevelCompletePanel : UIPanel
    {
        protected override void RegisterEvents()
        {
            GridEvents.GridComplete += OnGridComplete;
        }

        private void OnGridComplete()
        {
            SetActive(true);
        }

        protected override void UnRegisterEvents()
        {
            GridEvents.GridComplete -= OnGridComplete;
        }
    }
}
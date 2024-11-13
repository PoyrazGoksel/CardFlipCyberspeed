using Events;
using Extensions.Unity.MonoHelper;

namespace UI.Panels.Score
{
    public class ScorePanel : UIPanel
    {
        protected override void RegisterEvents()
        {
            GameEvents.LevelLoaded += OnLevelLoaded;
            GridEvents.GridComplete += OnGridComplete;
        }

        private void OnLevelLoaded()
        {
            SetActive(true);
        }

        private void OnGridComplete()
        {
            SetActive(false);
        }

        protected override void UnRegisterEvents()
        {
            GameEvents.LevelLoaded -= OnLevelLoaded;
            GridEvents.GridComplete -= OnGridComplete;
        }
    }
}
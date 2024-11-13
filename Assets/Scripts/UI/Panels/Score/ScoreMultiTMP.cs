using Events;
using Extensions.Unity.MonoHelper;

namespace UI.Panels.Score
{
    public class ScoreMultiTMP : UITMP
    {
        private const string ScoreMultiPrefix = "ScoreMulti: ";

        private void Start()
        {
            RenderScoreMulti(0);
        }

        private void RenderScoreMulti(int multi)
        {
            _myTMP.text = ScoreMultiPrefix + multi;
        }
        
        protected override void RegisterEvents()
        {
            PlayerEvents.ScoreMultiChanged += OnScoreMultiChanged;
        }

        private void OnScoreMultiChanged(int arg0)
        {
            RenderScoreMulti(arg0);
        }

        protected override void UnRegisterEvents()
        {
            PlayerEvents.ScoreMultiChanged -= OnScoreMultiChanged;
        }
    }
}
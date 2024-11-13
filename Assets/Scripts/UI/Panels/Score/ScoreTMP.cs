using Events;
using Extensions.Unity.MonoHelper;
using Installers;
using UnityEngine;

namespace UI.Panels.Score
{
    public class ScoreTMP : UITMP
    {
        private const string ScorePrefix = "Score: ";

        private void Start()
        {
            RenderScore(GameInstaller.Ins.PlayerVM.Score);
        }

        private void RenderScore(int multi)
        {
            _myTMP.text = ScorePrefix + multi;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(Application.isPlaying == false) return;
            
            if(isActive)
            {
                RenderScore(GameInstaller.Ins.PlayerVM.Score);
            }
        }

        protected override void RegisterEvents()
        {
            PlayerEvents.ScoreChanged += OnScoreMultiChanged;
        }

        private void OnScoreMultiChanged(int arg0)
        {
            RenderScore(arg0);
        }

        protected override void UnRegisterEvents()
        {
            PlayerEvents.ScoreChanged -= OnScoreMultiChanged;
        }
    }
}
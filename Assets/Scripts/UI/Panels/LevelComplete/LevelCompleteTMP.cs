using Events;
using Extensions.DoTween;
using Extensions.Unity.MonoHelper;
using UnityEngine;

namespace UI.Panels.LevelComplete
{
    public class LevelCompleteTMP : UITMP, ITweenContainerBind
    {
        public ITweenContainer TweenContainer{get;set;}

        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
        }

        protected override void RegisterEvents()
        {
            GridEvents.GridComplete += OnGridComplete;
        }

        private void OnGridComplete()
        {
            transform.DoYoYo(Vector3.one * 1.1f, 1f);
        }

        protected override void UnRegisterEvents()
        {
            GridEvents.GridComplete -= OnGridComplete;
        }
    }
}
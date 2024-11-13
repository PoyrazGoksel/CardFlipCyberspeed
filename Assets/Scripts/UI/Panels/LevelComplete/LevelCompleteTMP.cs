using Events;
using Extensions.DoTween;
using Extensions.Unity.MonoHelper;
using UnityEngine;

namespace UI.Panels.LevelComplete
{
    public class LevelCompleteTMP : UITMP, ITweenContainerBind
    {
        public ITweenContainer TweenContainer{get;set;}
        private Transform _transform;

        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
            _transform = transform;
        }

        protected override void RegisterEvents()
        {
            GridEvents.GridComplete += OnGridComplete;
        }

        private void OnGridComplete()
        {
            TweenContainer.AddTween = _transform.DoYoYo(Vector3.one * 1.1f, 1f); //TODO: Move to settings
        }

        protected override void UnRegisterEvents()
        {
            GridEvents.GridComplete -= OnGridComplete;
            TweenContainer.Clear();
        }
    }
}
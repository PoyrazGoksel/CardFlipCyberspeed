using Extensions.DoTween;
using Extensions.Unity.MonoHelper;
using UnityEngine;

namespace UI.Panels.LevelComplete
{
    public class FailEmojiIMG : UIIMG, ITweenContainerBind
    {
        private Transform _transform;
        
        public ITweenContainer TweenContainer{get;set;}

        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
            _transform = transform;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(Application.isPlaying == false) return;
            
            _transform.DoYoYo(Vector3.one * 1.1f, 1f);
        }

        protected override void UnRegisterEvents()
        {
            TweenContainer.Clear();
        }
    }
}
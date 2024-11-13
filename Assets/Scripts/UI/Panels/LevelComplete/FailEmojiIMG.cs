using Extensions.DoTween;
using Extensions.Unity.MonoHelper;
using UnityEngine;

namespace UI.Panels.LevelComplete
{
    public class FailEmojiIMG : UIIMG, ITweenContainerBind
    {
        public ITweenContainer TweenContainer{get;set;}

        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(Application.isPlaying == false) return;
            
            transform.DoYoYo(Vector3.one * 1.1f, 1f);
        }

        protected override void UnRegisterEvents()
        {
            TweenContainer.Clear();
        }
    }
}
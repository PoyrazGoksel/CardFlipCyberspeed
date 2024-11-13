using DG.Tweening;
using Extensions.DoTween;
using Extensions.Unity.MonoHelper;
using UI.Events;
using UnityEngine;

namespace UI.Panels.LevelComplete
{
    public class NextLevelBUT : UIButtonTMP, ITweenContainerBind
    {
        public ITweenContainer TweenContainer{get;set;}

        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
        }

        protected override void OnClick()
        {
            UIEvents.NextLevelClick?.Invoke();
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(Application.isPlaying == false) return;
            
            if(isActive)
            {
                _myImage.color = Color.white;
                
                TweenContainer.AddSequence = DOTween.Sequence();
                
                Tween highLightTween = _myImage.DOColor(new Color(0.5f, 1f, 0.5f), 0.5f);
                Tween whiteTween = _myImage.DOColor(Color.white, 0.5f);

                TweenContainer.AddedSeq.Append(highLightTween);
                TweenContainer.AddedSeq.Append(whiteTween);

                TweenContainer.AddedSeq.SetLoops(-1);
            }
        }

        protected override void UnRegisterEvents()
        {
            base.UnRegisterEvents();
            TweenContainer.Clear();
        }
    }
}
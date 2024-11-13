using System;
using DG.Tweening;
using Events;
using Extensions.DoTween;
using Extensions.Unity;
using Extensions.Unity.MonoHelper;
using Installers;
using Sirenix.OdinInspector;
using UnityEngine;
using ViewModels;

namespace Views
{
    public partial class CardView : EventListenerMono, ICard, ITweenContainerBind
    {
        public Vector3 InitPos => _initPos;
        [SerializeField] private int _id;
        [SerializeField] private Collider _collider;
        [SerializeField] private Vector2Int _coords;
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _initPos;
        private Settings _settings;
        private bool _isBusy;
        private bool _isDestroyed;
        private MainSceneVM MainSceneVM{get;set;}
        public int ID => _id;
        public Vector2Int Coords => _coords;
        public Bounds ColliderBounds => _collider.bounds;
        public ITweenContainer TweenContainer{get;set;}

        private void Awake()
        {
            MainSceneVM = GameInstaller.Ins.MainSceneVM;
            _settings = GameInstaller.Ins.GameSettings.CardSettings;
            TweenContainer = TweenContain.Install(this);
        }

        private void OnMouseDown()
        {
            if(_isDestroyed) return;
            if(MainSceneVM.InputEnabled == false) return;
            if(_isBusy) return;

            OpenCard();
        }

        private void OpenCard()
        {
            CardEvents.PreCardOpen?.Invoke();
            _isBusy = true;
            TweenContainer.AddSequence = DOTween.Sequence();

            Tween flipTween = _transform.DORotate(_settings.OpenRotEul, _settings.FlipAnimDur);
            
            Vector3 jumpPos = _initPos + new Vector3(0f, _settings.FlipJumpHeight, 0f);

            float animDurHalf = _settings.FlipAnimDur * 0.5f;
            Tween jumpTween = _transform.DOMove(jumpPos, animDurHalf);
            Tween dropTween = _transform.DOMove(_initPos, animDurHalf);

            TweenContainer.AddedSeq.Append(flipTween);
            TweenContainer.AddedSeq.Insert(0f, jumpTween);
            TweenContainer.AddedSeq.Insert(animDurHalf, dropTween);

            TweenContainer.AddedSeq.onComplete +=
            OnOpenAnimComplete;
        }

        private void OnOpenAnimComplete()
        {
            _isBusy = false;
            CardEvents.CardOpened?.Invoke(this);
        }

        private void CloseCard()
        {
            _isBusy = true;
            
            TweenContainer.AddSequence = DOTween.Sequence();

            Tween flipTween = _transform.DORotate(_settings.ClosedRotEul, _settings.FlipAnimDur);
            
            Vector3 jumpPos = _initPos + new Vector3(0f, _settings.FlipJumpHeight, 0f);

            float animDurHalf = _settings.FlipAnimDur * 0.5f;
            Tween jumpTween = _transform.DOMove(jumpPos, animDurHalf);
            Tween dropTween = _transform.DOMove(_initPos, animDurHalf);

            TweenContainer.AddedSeq.Append(flipTween);
            TweenContainer.AddedSeq.Insert(0f, jumpTween);
            TweenContainer.AddedSeq.Insert(animDurHalf, dropTween);

            TweenContainer.AddedSeq.onComplete += OnCloseAnimComplete;
        }

        private void OnCloseAnimComplete()
        {
            _isBusy = false;
        }

        [Button]
        private void DestroyCard()
        {
            _isBusy = true;
            
            TweenContainer.AddSequence = DOTween.Sequence();

            Tween scaleTweenUp = _transform.DOScale(Vector3.one * _settings.DestroySizeMax, _settings.DestroyScaleUpDur);
            Tween scaleTweenDown = _transform.DOScale(Vector3.zero, _settings.DestroyScaleDownDur);
            
            TweenContainer.AddedSeq.Append(scaleTweenUp);
            TweenContainer.AddedSeq.Append(scaleTweenDown);

            TweenContainer.AddedSeq.SetEase(_settings.DestroyAnimEase);
            
            TweenContainer.AddedSeq.onComplete += OnDestroyAnimComplete;
        }

        private void OnDestroyAnimComplete()
        {
            gameObject.Destroy();
        }

        protected override void RegisterEvents()
        {
            GridEvents.CardShowComplete += OnCloseCards;
        }

        private void OnCloseCards() {CloseCard();}

        public void Destroy()
        {
            _isDestroyed = true;
            DestroyCard();
        }

        public void Close()
        {
            CloseCard();
        }

        protected override void UnRegisterEvents()
        {
            GridEvents.CardShowComplete -= OnCloseCards;
            
            TweenContainer.Clear();
        }

        [Serializable]
        public class Settings
        {
            public Vector3 OpenRotEul => _openRotEul;
            public Vector3 ClosedRotEul => _closedRotEul;
            public float FlipJumpHeight => _flipJumpHeight;
            public float FlipAnimDur => _flipAnimDur;
            public Ease DestroyAnimEase => _destroyAnimEase;
            public float DestroySizeMax => _destroySizeMax;
            public float DestroyScaleUpDur => _destroyScaleUpDur = 0.1f;
            public float DestroyScaleDownDur => _destroyScaleDownDur = 0.4f;
            [SerializeField] private Vector3 _openRotEul = new(0f, 0f, 0f);
            [SerializeField] private Vector3 _closedRotEul = new(0f, 0f, -180f);
            [SerializeField] private float _flipJumpHeight = 1f;
            [SerializeField] private float _flipAnimDur = 0.5f;
            [SerializeField] private Ease _destroyAnimEase = Ease.Linear;
            [SerializeField] private float _destroySizeMax = 1.1f;
            [SerializeField] private float _destroyScaleUpDur;
            [SerializeField] private float _destroyScaleDownDur;
        }
    }

    public interface ICard
    {
        int ID{get;}
        Vector2Int Coords{get;}

        void Destroy();

        void Close();
    }
}
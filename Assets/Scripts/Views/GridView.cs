using System;
using Events;
using Extensions.Unity;
using Extensions.Unity.Entities;
using Extensions.Unity.MonoHelper;
using Installers;
using UnityEngine;

namespace Views
{
    public partial class GridView : EventListenerMono
    {
        [SerializeField] private CoordToCardDict _coordToCardDict = new();
        [SerializeField] private float _cardPaddingX = 0.25f;
        [SerializeField] private float _cardPaddingY = 0.25f;
        [SerializeField] private int _gridW;
        [SerializeField] private int _gridH;
        [SerializeField] private Transform _transform;
        [SerializeField] private Bounds _levelBounds;
        private Settings _settings;
        private ICard _lastCard;

        private void Awake()
        {
            _settings = GameInstaller.Ins.GameSettings.GridSettings;
        }

        private void Start()
        {
            GridEvents.GridStart?.Invoke(_levelBounds);
            this.WaitFor(new WaitForSeconds(_settings.CardShowDur), CardShowComplete);
        }

        private void CardShowComplete()
        {
            GridEvents.CardShowComplete?.Invoke();
        }

        private static Vector3 CoordToGridPos(float wPosX, float wPosY) => new(wPosX, 0f, wPosY);

        private Vector3 GetGridPos(int w, int h, float cardSizeX, float cardSizeZ)
        {
            float wPosX = (w * cardSizeX) + (w * _cardPaddingX);
            float wPosY = (h * cardSizeZ) + (h * _cardPaddingY);
                
            return CoordToGridPos(wPosX, wPosY);
        }

        private void CompareCards(ICard arg0)
        {
            if(_lastCard.ID == arg0.ID)
            {
                _coordToCardDict[_lastCard.Coords] = null;
                _coordToCardDict[arg0.Coords] = null;

                _lastCard.Destroy();
                arg0.Destroy();
            }
            else
            {
                _lastCard.Close();
                arg0.Close();
            }
            
            _lastCard = null;
        }

        protected override void RegisterEvents()
        {
            CardEvents.CardOpened += OnCardOpened;
        }

        private void OnCardOpened(ICard arg0)
        {
            if(_lastCard == null)
            {
                _lastCard = arg0;
            }
            else
            {
                CompareCards(arg0);
            }
        }

        protected override void UnRegisterEvents()
        {
            CardEvents.CardOpened -= OnCardOpened;
        }

        [Serializable]
        public class Settings
        {
            public float CardShowDur => _cardShowDur = 1f;
            [SerializeField] private float _cardShowDur;
        }
    }

    [Serializable]
    public class CoordToCardDict : UnityDictionary<Vector2Int, CardView>{}
}
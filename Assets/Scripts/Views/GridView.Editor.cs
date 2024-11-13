#if UNITY_EDITOR
using System.Collections.Generic;
using Extensions.System;
using Extensions.Unity;
using Settings;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Views
{
    public partial class GridView
    {
        private GameSettings _editorGameSettings;

        private void LoadGameSettingsInEditor()
        {
            _editorGameSettings = Resources.Load<GameSettings>
            (EnvVar.SettingsPath + nameof(GameSettings));
        }

        [Button]
        private void CreateGrid(int width, int height)
        {
            if((width * height) % 2 != 0)
            {
                Debug.LogWarning($"Card count in grid must be dividable by 2");
                return;
            }

            _gridW = width;
            _gridH = height;
            
            DeletePrevGrid();
            
            CreateCoords();
            
            LoadGameSettingsInEditor();
            
            List<ICardEditor> cardsCreated = CreateCards();
            
            cardsCreated.Shuffle();

            AssignCardsToCoords(cardsCreated);

            CalcBounds();
        }

        private void DeletePrevGrid()
        {
            foreach(ICardEditor cardView in _coordToCardDict._tValues)
            {
                if(cardView != null)
                {
                    cardView.gameObject.DestroyNow();
                }
            }

            _coordToCardDict = new CoordToCardDict();
        }

        private void CreateCoords()
        {
            for(int w = 0; w < _gridW; w ++)
            for(int h = 0; h < _gridH; h ++)
            {
                _coordToCardDict.Add(new Vector2Int(w, h), null);
            }
        }

        private List<ICardEditor> CreateCards()
        {
            List<ICardEditor> cardsToCreate = new();
            
            for(int i = 0; i < _coordToCardDict.Count / 2; i ++)
            {
                GameObject randCardPref = _editorGameSettings.GetRandomCardPrefab();

                ICardEditor card1 = InstantiateCard(randCardPref);
                cardsToCreate.Add(card1);
                
                ICardEditor card2 = InstantiateCard(randCardPref);
                cardsToCreate.Add(card2);
            }

            return cardsToCreate;
        }

        private void AssignCardsToCoords(List<ICardEditor> cardsCreated)
        {
            int index = 0;

            for(int w = 0; w < _gridW; w ++)
            for(int h = 0; h < _gridH; h ++)
            {
                ICardEditor thisCard = cardsCreated[index];
                Vector2Int thisCoord = new(w, h);
                _coordToCardDict[thisCoord] = thisCard.GetConcreteType();
                
                index ++;
                
                thisCard.SetCoords(thisCoord);

                thisCard.SetPos
                (
                    GetGridPos
                    (
                        w,
                        h,
                        thisCard.ColliderBounds.size.x,
                        thisCard.ColliderBounds.size.z
                    )
                );
            }
        }

        [Button]
        private void CalcBounds()
        {
            _levelBounds = new Bounds();

            foreach(ICardEditor cardEditor in _coordToCardDict._tValues)
            {
                _levelBounds.Encapsulate(cardEditor.ColliderBounds);
            }
        }

        private ICardEditor InstantiateCard(GameObject randCardPref)
        {
            ICardEditor card1
            = PrefabUtility.InstantiatePrefab(randCardPref, _transform).GetComponent<ICardEditor>();

            return card1;
        }
    }
}
#endif
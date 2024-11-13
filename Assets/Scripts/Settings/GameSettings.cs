using System.Collections.Generic;
using Extensions.System;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Views;

namespace Settings
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = EnvVar.GameName + "/" + nameof(GameSettings), order = 0)]
    public class GameSettings : ScriptableObject
    {
        public CardView.Settings CardSettings => _cardSettings;
        public GridView.Settings GridSettings => _gridSettings;
        public AudioPlayer.Settings AudioPlayerSettings => _audioPlayerSettings;
        [SerializeField] private List<GameObject> _cardPrefabsByID = new();
        [SerializeField] private List<GameObject> _levelsByID = new();
        [SerializeField] private CardView.Settings _cardSettings;
        [SerializeField] private GridView.Settings _gridSettings;
        [SerializeField] private AudioPlayer.Settings _audioPlayerSettings;
#if UNITY_EDITOR
        [Button]
        public void AssignIndexToCardIDs(bool areYouSure)
        {
            if(areYouSure == false) return;

            for(int i = 0; i < _cardPrefabsByID.Count; i ++)
            {
                GameObject cardPrefab = _cardPrefabsByID[i];
                ICardEditor thisCardView = cardPrefab.GetComponent<ICardEditor>();
                
                thisCardView.SetID(i);

                EditorUtility.SetDirty(cardPrefab);
            }
        }
#endif

        public GameObject GetCard(int id)
        {
            if(id >= _cardPrefabsByID.Count)
            {
                Debug.LogWarning($"Trying to get non existent index/CardID = {id}");
                
                return null;
            }
            
            return _cardPrefabsByID[id];
        }

        public GameObject GetRandomCardPrefab()
        {
            return _cardPrefabsByID.Random();
        }

        public int GetLevelCount()
        {
            return _levelsByID.Count;
        }

        public GameObject GetLevel(int id)
        {
            if(id >= _levelsByID.Count)
            {
                Debug.LogWarning($"Trying to get non existent index/LevelID = {id}");
                
                return null;
            }
            
            return _levelsByID[id];
        }
    }
}
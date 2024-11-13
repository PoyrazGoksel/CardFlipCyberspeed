using System.Collections.Generic;
using Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = EnvVar.GameName + "/" + nameof(GameSettings), order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private List<GameObject> _cardPrefabsByID = new();

        [Button]
        public void AssignIndexToCardIDs(bool areYouSure)
        {
            if(areYouSure == false) return;

            for(int i = 0; i < _cardPrefabsByID.Count; i ++)
            {
                GameObject cardPrefab = _cardPrefabsByID[i];
                Card thisCard = cardPrefab.GetComponent<Card>();
                
                thisCard.SetID(i);
            }
        }
    }
}
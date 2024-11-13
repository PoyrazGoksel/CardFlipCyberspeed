using System.IO;
using Events;
using Extensions.Unity.Utils;
using Models;
using UnityEngine;

namespace ViewModels
{
    public class PlayerVM
    {
        private static string PersistentDataPath => Application.persistentDataPath + "/" + nameof(PlayerModel) + EnvVar.SaveExt;
        public int Score => _score;
        public int ScoreMulti => _scoreMulti;

        public int Level
        {
            get => _playerModel.Level;
            set
            {
                _playerModel.Level = value;
                Save();
            }
        }

        private PlayerModel _playerModel;
        private int _score;
        private int _scoreMulti;

        public PlayerVM()
        {
            RegisterEvents();
            Load();
        }

        private void Load()
        {
            if(File.Exists(PersistentDataPath) == false)
            {
                _playerModel = new PlayerModel();
                Save();
            }
            else
            {
                string jsonStr = JsonUtilityWithCall.ReadToEnd(PersistentDataPath);
                _playerModel = JsonUtilityWithCall.FromJson<PlayerModel>(jsonStr);
            }
        }

        private void LevelUp()
        {
            Level ++;
        }

        private void Save()
        {
            if(_playerModel == null)
            {
                Debug.LogWarning("Trying to save null playermodel!");
                return;
            }
            
            string jsonStr = JsonUtilityWithCall.ToJson(_playerModel);
            
            JsonUtilityWithCall.WriteToEnd(jsonStr, PersistentDataPath);
        }

        private void RegisterEvents()
        {
            GridEvents.GridComplete += OnGridComplete;
        }

        private void OnGridComplete()
        {
            LevelUp();
        }
    }
}
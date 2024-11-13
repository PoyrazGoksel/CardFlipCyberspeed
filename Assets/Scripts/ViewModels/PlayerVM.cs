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

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                
                PlayerEvents.ScoreChanged?.Invoke(_score);
            }
        }

        public int ScoreMulti
        {
            get => _scoreMulti;
            private set
            {
                _scoreMulti = value;
                
                PlayerEvents.ScoreMultiChanged?.Invoke(_scoreMulti);
            }
        }

        public int Level
        {
            get => _playerModel.Level;
            private set
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
            
            PlayerEvents.PlayerLoaded?.Invoke();
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
            GridEvents.Match += OnMatch;
            GridEvents.MatchFail += OnMatchFail;
            GameEvents.PreLevelLoaded += OnPreLevelLoaded;
        }

        private void OnMatch()
        {
            ScoreMulti++;
            
            Score += 2 * ScoreMulti;
        }

        private void OnMatchFail()
        {
            ScoreMulti = 0;
        }

        private void OnPreLevelLoaded()
        {
            Score = 0;
            ScoreMulti = 0;
        }

        private void OnGridComplete()
        {
            LevelUp();
        }
    }
}
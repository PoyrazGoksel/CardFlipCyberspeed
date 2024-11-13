using Events;
using Extensions.Unity.MonoHelper;
using Settings;
using UnityEngine;
using ViewModels;

namespace Installers
{
    public class MainSceneInstaller : EventListenerMono
    {
        private GameObject _currLevel;
        private GameSettings GameSettings{get;set;}
        private PlayerVM PlayerVM{get;set;}
        
        private void LoadLevel()
        {
             _currLevel = GameSettings.GetLevel(PlayerVM.Level);

             Instantiate(_currLevel);
        }

        protected override void RegisterEvents()
        {
            GameEvents.GameStarted += OnGameStarted;
        }

        private void OnGameStarted()
        {
            GameSettings = GameInstaller.Ins.GameSettings;
            PlayerVM = GameInstaller.Ins.PlayerVM;

            GameEvents.PreLevelLoaded?.Invoke();
            LoadLevel();
        }

        protected override void UnRegisterEvents()
        {
            GameEvents.GameStarted -= OnGameStarted;
        }
    }
}
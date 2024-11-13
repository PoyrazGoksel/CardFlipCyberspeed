using Events;
using Settings;
using UnityEngine;
using ViewModels;

namespace Installers
{
    public class GameInstaller
    {
        public static GameInstaller Ins;
        public GameSettings GameSettings{get; private set;}
        public PlayerVM PlayerVM{get; private set;}
        public MainSceneVM MainSceneVM{get;private set;}

        private GameInstaller()
        {
            RegisterEvents();
            
            InstallSettings();
            InstallViewModels();
            
            GameEvents.GameStarted?.Invoke();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BeforeFirstScene()
        {
            if(Ins == null)
            {
                Ins = new GameInstaller();
            }
            else
            {
                Debug.LogWarning("Trying to initialize GameInstaller twice?");
            }
        }

        private void InstallSettings()
        {
            GameSettings = Resources.Load<GameSettings>
            (EnvVar.SettingsPath + nameof(Settings.GameSettings));
        }

        private void InstallViewModels()
        {
            PlayerVM = new PlayerVM();
            GameEvents.PlayerLoaded?.Invoke();
        }

        private void RegisterEvents()
        {
            GameEvents.PreLevelLoaded += OnNewLevelLoaded;
        }

        private void OnNewLevelLoaded()
        {
            MainSceneVM = new MainSceneVM();
        }
    }
}
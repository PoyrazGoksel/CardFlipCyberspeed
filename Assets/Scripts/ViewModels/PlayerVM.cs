using Models;

namespace ViewModels
{
    public class PlayerVM
    {
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
            Load();
        }

        private void Load()
        {
            _playerModel = new PlayerModel();
        }

        private void Save() {}
    }
}
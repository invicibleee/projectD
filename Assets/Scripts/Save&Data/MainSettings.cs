
using System.Numerics;

namespace SaveData
{
    [System.Serializable]
    public class MainSettings
    {
        public bool _isFullScreen;
        public int _currentQualityLevel;
        public float _volumeValue;

        public int _width;
        public int _height;

        public bool[] _isOwned;

        public MainSettings()
        {
            _isFullScreen = true;
            _volumeValue = 0.5f;
            _currentQualityLevel = 2;
            _width = 1920;
            _height = 1080;
        }
    }
    [System.Serializable]
    public class CharaStatistic
    {
        public int _maxFlask;
        public int _currentFlask;
        public float _currentHealth;
        public float _maxHealth;
        public float _currentMana;
        public float _maxMana;
        public float _currentUlt;
        public CharaStatistic()
        {
            _maxFlask = 3;
            _currentFlask = _maxFlask;
            _maxMana = 100;
            _maxHealth = 100;
            _currentUlt = 0; 
            _currentMana = 0;
            _currentHealth = _maxHealth;
        }
    }
    [System.Serializable]
    public class MoneyPlayer
    {
        public int _money;

        public MoneyPlayer()
        {
            _money = 1000;
        }
    }
    [System.Serializable]
    public class PlayerPos
    {
        public UnityEngine.Vector2 _playerPos; // явно вказуЇмо UnityEngine.Vector2
        public int _sceneIndex;

        public PlayerPos()
        {
            _playerPos = new UnityEngine.Vector2(-3, 12); // явно вказуЇмо UnityEngine.Vector2
            _sceneIndex = 1;
        }
    }
    [System.Serializable]
    public class WeaponSave
    {
        public bool[] _isOwned = new bool[9];
        public int _isEqiped;
        public WeaponSave()
        {
            _isEqiped = -1;
            for (int i = 0; i < _isOwned.Length; i++)
            {
                _isOwned[i] = false;
            }
        }
    }
}



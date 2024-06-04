
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
            _maxHealth = 200;
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
            _money = 0;
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

    [System.Serializable]
    public class AbilitySave
    {
        public bool[] _isOwned = new bool[2];
        public int _isEqiped;
        public AbilitySave()
        {
            _isEqiped = -1;
            for (int i = 0; i < _isOwned.Length; i++)
            {
                _isOwned[i] = false;
            }
        }
    }

    [System.Serializable]
    public class CharmsSave
    {
        public bool[] _isOwned = new bool[14];
        public int[] _isEqiped = new int[3];
        public CharmsSave()
        {
            for (int i = 0; i < _isEqiped.Length; i++)
            {
                _isEqiped[i] = -1;
            }
            for (int i = 0; i < _isOwned.Length; i++)
            {
                _isOwned[i] = false;
            }
        }
    }

    [System.Serializable]
    public class TalantsSave
    {
        public bool[] _isOwned = new bool[5];
        public TalantsSave()
        {
            for (int i = 0; i < _isOwned.Length; i++)
            {
                _isOwned[i] = false;
            }
        }
    }

    [System.Serializable]
    public class ItemSave
    {
        public bool[] _isOwned = new bool[12];
        public ItemSave()
        {
            for (int i = 0; i < _isOwned.Length; i++)
            {
                _isOwned[i] = false;
            }
        }
    }
    [System.Serializable]
    public class LostMoneySave
    {
        public int _amount;
        public int _sceneIndex;
        public UnityEngine.Vector2 _position;
        public LostMoneySave()
        {
            _amount = 0;
            _sceneIndex = 1;
            _position = new UnityEngine.Vector2(0,0);
        }
    }
    [System.Serializable]
    public class LostStatusSave
    {
        public bool _status;
        public LostStatusSave()
        {
            _status = false;
        }
    }

    [System.Serializable]
    public class StatueSave
    {
        public UnityEngine.Vector2 _statuePosition;
        public int _sceneIndex;
        public StatueSave()
        {
            _statuePosition = new UnityEngine.Vector2(-3, 12);
            _sceneIndex = 1;
        }
    }
    [System.Serializable]
    public class CameraActivity
    {
        public bool _isMainCameraActive1;
        public bool _isMainCameraActive2;
        public bool _isMainCameraActive3;
        public bool _isMainCameraActive4;
        public CameraActivity()
        {
          _isMainCameraActive1 = true;
          _isMainCameraActive2 = false;
          _isMainCameraActive3 = true;    
          _isMainCameraActive4 = false;

        }
    }
    [System.Serializable]
    public class BossSave
    {
        public bool _isFirstBossAlive;
        public bool _isSecondBossAlive;
        public BossSave()
        {
            _isFirstBossAlive = true;
            _isSecondBossAlive = true;
        }
    }

    [System.Serializable]
    public class DoorSave
    {
        public bool _doorOpenOne;
        public bool _doorOpenTwo;
        public bool _doorOpenThree;
        public DoorSave()
        {
            _doorOpenOne = false;
            _doorOpenTwo = false;
            _doorOpenThree = false;
        }
    }

    [System.Serializable]
    public class MapSave
    {
        public bool _isForestOpen;
        public bool _isCastleOpen;

        public MapSave()
        {
            _isForestOpen = false;
            _isCastleOpen = false;
        }
    }

    [System.Serializable]
    public class IconsSave
    {
        public bool _isForestBossVisited;
        public bool _isCastleBossVisited;
        public bool _isSnakeVisited;

        public IconsSave()
        {
            _isForestBossVisited = false;
            _isCastleBossVisited = false;
            _isSnakeVisited = false;
        }
    }

    [System.Serializable]
    public class NPCDialogues
    {
        public bool _isFirstPilgrimDialogue;
        public bool _isFirstSnakeDialogue;
        public bool _isFirstMushroomDialogue;
        public bool _infuserOwned;
        public bool _isDogQuestCompleted;
        public NPCDialogues()
        {
            _isFirstMushroomDialogue = true;
            _isFirstSnakeDialogue = true;
            _isFirstPilgrimDialogue = true;
            _infuserOwned = false;
            _isDogQuestCompleted = false;
        }
    }

    [System.Serializable]
    public class TutorialSave
    {
        public bool _tutorailShowed;
        public TutorialSave()
        {
            _tutorailShowed = false;
        }
    }
    [System.Serializable]
    public class AchivementsSave
    {
        public bool[] _isOwned = new bool[14];
        public AchivementsSave()
        {
            for (int i = 0; i < _isOwned.Length; i++)
            {
                _isOwned[i] = false;
            }
        }
    }
}



using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    private Player player;
    private bool isDead;
    private HealthFlask healthFlask;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float delayBeforePause = 1f; // Змінна для налаштування затримки
    [SerializeField] private MoneyTrigger currency;   
    [SerializeField] private GameObject money;
    [SerializeField] private BarsController barsController;
    [SerializeField] private int pauseTime = 3;
    private Vector2 playerDeathPoint;
    private int lostCurrency;
    private bool status = false;
    private int index;
    private MoneyTrigger moneyTrigger;

    private string saveKey = "LostMoney";
    private string saveKey2 = "LostStatusMoney";
    private string saveKey3 = "StatueSave";
    private string saveKey4 = "playerPosition";
    private string saveKey5 = "camera";
    private void Start()
    {
        Load();
        player = FindAnyObjectByType<Player>();
        barsController = FindAnyObjectByType<BarsController>();
        healthFlask = FindAnyObjectByType<HealthFlask>();
        moneyTrigger = FindAnyObjectByType<MoneyTrigger>();
    }

    private void Update()
    {
        if (player.stats.isDead)
        {
            playerDeathPoint = player.transform.position;
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }



    public void Respawn()
    {
        player.stats.isDead = false;
        lostCurrency = PlayerManager.instance.essenceAmount;
        PlayerManager.instance.RemoveEssences(lostCurrency);
        deathScreen.SetActive(false);
        money.transform.position = playerDeathPoint;
        
        status = true;

        money.SetActive(status);
        Save2();
        currency.GetCurrency(lostCurrency);
        player.stats.currentHealth = player.stats.maxHealth.GetValue();
        healthFlask.currentFlasks = healthFlask.maxFlasks;
        healthFlask.Save();
        healthFlask.Load();
        barsController.SetHealth(player.stats.currentHealth, player.stats.maxHealth.GetValue());
        index = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        Save();
        Save4();
        Load2();
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }
    public void Save2()
    {
        SaveManager.Save(saveKey2, GetData2());
    }
    public void Save3()
    {
        SaveManager.Save(saveKey4, GetData3());
    }
    public void Save4()
    {
        SaveManager.Save(saveKey5, GetData4());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.LostMoneySave>(saveKey);
        lostCurrency = data._amount;
        playerDeathPoint = data._position;
        currency.GetCurrency(lostCurrency);
        money.transform.position = playerDeathPoint;
    }
    private void Load2()
    {
        var data = SaveManager.Load<SaveData.StatueSave>(saveKey3);
        player.transform.position = data._statuePosition;
        Save3();
        SceneManager.LoadScene(data._sceneIndex);
    }

    private SaveData.LostMoneySave GetData()
    {
        var data = new SaveData.LostMoneySave()
        {
            _amount = lostCurrency,
            _position = playerDeathPoint,
            _sceneIndex = index,
        };

        return data;
    }

    private SaveData.LostStatusSave GetData2()
    {
        var data = new SaveData.LostStatusSave()
        {
            _status = status,
        };

        return data;
    }

    private SaveData.PlayerPos GetData3()
    {
        var data = new SaveData.PlayerPos()
        {
            _playerPos = player.transform.position,
        };

        return data;
    }
    private SaveData.CameraActivity GetData4()
    {
        var data = new SaveData.CameraActivity()
        {  
            _isMainCameraActive1 = true,
            _isMainCameraActive2 = false,
            _isMainCameraActive3 = true,
            _isMainCameraActive4 = false,
        };

        return data;
    }
}

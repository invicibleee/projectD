using System.Collections;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    private Player player;
    private bool isDead;
    private HealthFlask healthFlask;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float delayBeforePause = 1f; // ����� ��� ������������ ��������
    [SerializeField] private MoneyTrigger currency;   
    [SerializeField] private GameObject money;
    [SerializeField] private BarsController barsController;
    private Vector2 playerDeathPoint;
    private int lostCurrency;
    private bool status = false;
    private MoneyTrigger moneyTrigger;

    private string saveKey = "LostMoney";
    private string saveKey2 = "LostStatusMoney";
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
        player.transform.position = transform.position;
        Save();
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }
    public void Save2()
    {
        SaveManager.Save(saveKey2, GetData2());
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.LostMoneySave>(saveKey);
        lostCurrency = data._amount;
        playerDeathPoint = data._position;
        currency.GetCurrency(lostCurrency);
        money.transform.position = playerDeathPoint;
    }

    private SaveData.LostMoneySave GetData()
    {
        var data = new SaveData.LostMoneySave()
        {
            _amount = lostCurrency,
            _position = playerDeathPoint,
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
}

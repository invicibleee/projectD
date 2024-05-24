using System.Collections;
using UnityEngine;

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
    private Vector2 playerDeathPoint;
    private int lostCurrency;
    private bool status;
    private MoneyTrigger moneyTrigger;

    private string saveKey = "LostMoney";
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
        money.SetActive(true);
        currency.GetCurrency(lostCurrency);
        player.stats.currentHealth = player.stats.maxHealth.GetValue();
        healthFlask.currentFlasks = healthFlask.maxFlasks;
        healthFlask.Save();
        healthFlask.Load();
        barsController.SetHealth(player.stats.currentHealth, player.stats.maxHealth.GetValue());
        player.transform.position = transform.position;
        status = true;
        moneyTrigger.status = status;
        Save();
        
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());
    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.LostMoneySave>(saveKey);
        lostCurrency = data._amount;
        playerDeathPoint = data._position;
        money.transform.position = playerDeathPoint;
        money.SetActive(data._status);
        currency.GetCurrency(lostCurrency);
    }

    private SaveData.LostMoneySave GetData()
    {
        var data = new SaveData.LostMoneySave()
        {
            _amount = lostCurrency,
            _position = playerDeathPoint,
            _status = status,
        };

        return data;
    }
}

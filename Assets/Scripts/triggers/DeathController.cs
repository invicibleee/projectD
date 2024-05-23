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
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        barsController = FindAnyObjectByType<BarsController>();
        healthFlask = FindAnyObjectByType<HealthFlask>();
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
    }

}

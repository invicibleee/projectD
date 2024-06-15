using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BossGUI : MonoBehaviour
{
    private AbilitiesPanelScript abilitiesPanelScript;
    [SerializeField] private EnemyBossOne bossOne;
    [SerializeField] public GameObject HUD;
    [SerializeField] private Image BarHP;
    [SerializeField] private GameObject Boss;
    public float maxHP;
    public float currentHP;
    [SerializeField] public bool isDead;

    [SerializeField] private float displayTime = 10f;
    [SerializeField] private float fadeDuration = 4f;

    [SerializeField] private GameObject hpBack;
    [SerializeField] private GameObject hp;
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject victoryText;
    [SerializeField] private GameObject exitTrigger;
    [SerializeField] private GameObject[] walls;
    [SerializeField] public int bossIndex;
    [SerializeField] private GameObject bossIcon;
    private bool isBossOpened;

    private string saveKey = "BossSave";
    private string saveKey2 = "IconsSave";
    private string saveKey3 = "achivementsSave";
    private SaveData.AchivementsSave data2;

    private void Awake()
    {
        Load();
        Load2();
        if (!isDead)
        {
            Boss.SetActive(true);
            bossOne = FindAnyObjectByType<EnemyBossOne>();
        }
        abilitiesPanelScript = FindAnyObjectByType<AbilitiesPanelScript>();
        data2 = SaveManager.Load<SaveData.AchivementsSave>(saveKey3);
    }
    void Start()
    {
        maxHP = 1;

        currentHP = maxHP;
        ResetHUDColors();
  
    }

    void UpdateBar()
    {
        BarHP.fillAmount = currentHP / maxHP;
    }

    public void SetStatus(bool Dead)
    {
        isDead = Dead;
    }
    public bool GetStatus(bool Dead)
    {
        isDead = Dead;
        return Dead;
    }

    public async void FixedUpdate()
    {
        if (!isDead)
        {
            currentHP = bossOne.stats.currentHealth;
            UpdateBar();
        }


        if (currentHP <= 0 && HUD.activeSelf)
        {
            if (!data2._isOwned[3])
            {
                MainMenu.instance.setAchivementOwned(3);
            }
            UpdateBar();
            isDead = true;
            Save();
            victoryText.SetActive(true);
            walls[0].SetActive(false);
            walls[1].SetActive(false);
            exitTrigger.SetActive(true);
            abilitiesPanelScript.SetAbilityOwned(1);
            await FadeOutHUD(displayTime);
           
            HUD.SetActive(false);
      
        }

    }

    private async Task FadeOutHUD(float duration)
    {
        Color[] originalColors = new Color[4];
        originalColors[0] = hpBack.GetComponent<Image>().color;
        originalColors[1] = hp.GetComponent<Image>().color;
        originalColors[2] = nameText.GetComponent<Text>().color;
        originalColors[3] = victoryText.GetComponent<Text>().color;

        Color[] fadeColors = new Color[4];
        for (int i = 0; i < 4; i++)
        {
            fadeColors[i] = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, 0f);
        }

        float timer = 0f;
        if(HUD.activeSelf == true && HUD != null) {
            while (timer < duration)
            {
                float progress = timer / duration * 5f;

                hpBack.GetComponent<Image>().color = Color.Lerp(originalColors[0], fadeColors[0], progress);
                hp.GetComponent<Image>().color = Color.Lerp(originalColors[1], fadeColors[1], progress);
                nameText.GetComponent<Text>().color = Color.Lerp(originalColors[2], fadeColors[2], progress);
                victoryText.GetComponent<Text>().color = Color.Lerp(originalColors[3], fadeColors[3], progress);

                timer += Time.deltaTime;
                await Task.Yield();
            }
        }


    }

    void ResetHUDColors()
    {
        Image hpBackImage = hpBack.GetComponent<Image>();
        Image hpImage = hp.GetComponent<Image>();
        Text nameTextComponent = nameText.GetComponent<Text>();
        Text victoryTextComponent = victoryText.GetComponent<Text>();

        Color hpBackColor = hpBackImage.color;
        hpBackColor.a = 1f;
        hpBackImage.color = hpBackColor;

        Color hpColor = hpImage.color;
        hpColor.a = 1f;
        hpImage.color = hpColor;

        Color nameColor = nameTextComponent.color;
        nameColor.a = 1f;
        nameTextComponent.color = nameColor;

        Color victoryColor = victoryTextComponent.color;
        victoryColor.a = 1f;
        victoryTextComponent.color = victoryColor;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            HUD.SetActive(true);
            walls[0].SetActive(true);
            walls[1].SetActive(true);
            exitTrigger.SetActive(false);

            if (bossOne != null)
            {
                maxHP = bossOne.stats.maxHealth.GetValue();

                currentHP = maxHP;
            }

            isBossOpened = true;
            bossIcon.SetActive(true);
            Save2();
        }

    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }
    private void Load()
    {
        var data = SaveManager.Load<SaveData.BossSave>(saveKey);
        isDead = !data._isFirstBossAlive;

    }
    private SaveData.BossSave GetData()
    {
        var data = new SaveData.BossSave()
        {
            _isFirstBossAlive = !isDead,
        };
        return data;
    }

    public void Save2()
    {
        Debug.Log("Saving boss icon state: " + isBossOpened);
        SaveManager.Save(saveKey2, GetData2());
    }
    private void Load2()
    {
        var data = SaveManager.Load<SaveData.IconsSave>(saveKey2);
        isBossOpened = data._isForestBossVisited;
        bossIcon.SetActive(isBossOpened);
        Debug.Log("Loaded boss icon state: " + isBossOpened);
    }
    private SaveData.IconsSave GetData2()
    {
        var data = new SaveData.IconsSave()
        {
            _isForestBossVisited = isBossOpened,
        };
        return data;
    }

}

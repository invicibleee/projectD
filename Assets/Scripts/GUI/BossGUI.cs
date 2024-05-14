using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class BossGUI : MonoBehaviour
{
    protected bool isDead;
    [SerializeField] private GameObject HUD;
    [SerializeField] private Image BarHP;
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP;

    [SerializeField] private float displayTime = 10f;
    [SerializeField] private float fadeDuration = 4f;

    [SerializeField] private GameObject hpBack;
    [SerializeField] private GameObject hp;
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject victoryText;

    void Start()
    {
        currentHP = maxHP;
        ResetHUDColors();
    }
    void UpdateBar()
    {
        BarHP.fillAmount = currentHP / maxHP;
    }

    public bool GetStatus(bool Dead)
    {
        Dead = isDead;
        return Dead;
    }
    public void SetHealth(float health)
    {
        currentHP = Mathf.Clamp(health, 0f, maxHP);
        UpdateBar();
    }
    public async void FixedUpdate()
    {
        if (currentHP <= 0 && HUD.activeSelf)
        {
            UpdateBar();
            isDead = true;
            victoryText.SetActive(true);

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
        while (timer < duration)
        {
            float progress = timer / duration;

            hpBack.GetComponent<Image>().color = Color.Lerp(originalColors[0], fadeColors[0], progress);
            hp.GetComponent<Image>().color = Color.Lerp(originalColors[1], fadeColors[1], progress);
            nameText.GetComponent<Text>().color = Color.Lerp(originalColors[2], fadeColors[2], progress);
            victoryText.GetComponent<Text>().color = Color.Lerp(originalColors[3], fadeColors[3], progress);

            timer += Time.deltaTime;
            await Task.Yield();
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
        }
    }

}

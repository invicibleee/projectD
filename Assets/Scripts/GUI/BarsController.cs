using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class BarsController : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    [SerializeField] private Image BarHP;
    [SerializeField] private Image BarMana;
    [SerializeField] private Image BarUlt;

    private float currentHP;
    private float currentMana;
    private float currentUlt;


    void Start()
    {
        currentHP = stats.currentHealth;
        currentMana = stats.currentMana;
        currentUlt = stats.currentUlt;
        UpdateBars();
    }

    public void UpdateBars()
    {
        BarHP.fillAmount = currentHP / stats.GetMaxHealthValue();
        BarMana.fillAmount = currentMana / stats.GetMaxManaValue();
        BarUlt.fillAmount = currentUlt / stats.GetMaxUltValue();
    }

    public void SetHealth(float health, float maxHealth)
    {
        currentHP = Mathf.Clamp(health, 0f, maxHealth);
        UpdateBars();
    }

    public void SetMana(float mana, float maxMana)
    {
        currentMana = Mathf.Clamp(mana, 0f, maxMana);
        UpdateBars();
    }

    public void SetUlt(float ult, float maxUlt)
    {
        currentUlt = Mathf.Clamp(ult, 0f, maxUlt);
        UpdateBars();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateBars();
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaUltBars : MonoBehaviour
{
    [SerializeField] private CharacterStats stats;
    public Image BarHP;
    public Image BarMana;
    public Image BarUlt;

    public float maxMana = 100;
    public float maxUlt = 100;

    private float currentHP;
    private float currentMana;
    private float currentUlt;

   //  public float currentHP;
   // public float currentMana;
   // public float currentUlt;
    void Start()
    {
        currentHP = stats.GetMaxHealthValue();
        Debug.Log(currentHP);
        currentMana = maxMana;
        currentUlt = maxUlt;
        UpdateBars();
    }

    void UpdateBars()
    {
        BarHP.fillAmount = currentHP / stats.GetMaxHealthValue();
        BarMana.fillAmount = currentMana / maxMana;
        BarUlt.fillAmount = currentUlt / maxUlt;
    }

    public void SetHealth(float health, float maxHealth)
    {
        currentHP = Mathf.Clamp(health, 0f, maxHealth);
        UpdateBars();
    }

    public void SetMana(float mana)
    {
        currentMana = Mathf.Clamp(mana, 0f, maxMana);
        UpdateBars();
    }

    public void SetUlt(float ult)
    {
        currentUlt = Mathf.Clamp(ult, 0f, maxUlt);
        UpdateBars();
    }

    public void IncreaseUltPercent(float percent)
    {
        float ultIncrease = (percent / 100f) * maxUlt;
        SetUlt(currentUlt + ultIncrease);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateBars();
    }
#endif
}

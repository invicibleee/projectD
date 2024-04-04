using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaUltBars : MonoBehaviour
{
    public Image BarHP;
    public Image BarMana;
    public Image BarUlt;

    public float maxHP = 100f;
    public float maxMana = 100f;
    public float maxUlt = 100f;

    private float currentHP;
    private float currentMana;
    private float currentUlt;

   //  public float currentHP;
   // public float currentMana;
   // public float currentUlt;
    void Start()
    {
        currentHP = maxHP;
        currentMana = maxMana;
        currentUlt = maxUlt;
        UpdateBars();
    }

    void UpdateBars()
    {
        BarHP.fillAmount = currentHP / maxHP;
        BarMana.fillAmount = currentMana / maxMana;
        BarUlt.fillAmount = currentUlt / maxUlt;
    }

    public void SetHealth(float health)
    {
        currentHP = Mathf.Clamp(health, 0f, maxHP);
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

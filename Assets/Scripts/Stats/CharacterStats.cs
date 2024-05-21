using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major stats")]
    public Stat strength; // 1 pofloat increase damage by 1 and crit.power by 1%
    public Stat agility;  // 1 pofloat increase evasion by 1% and crit.chance by 1%
    public Stat intelligence; // 1 pofloat increase magic damage by 1 and magic resistance by 3
    public Stat vitality; // 1 pofloat incredase health by 3 or 5 pofloats

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;
    public Stat magicAmplify;// default value 150%

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;

    [Header("Mana stats")]
    public Stat maxMana;
    public Stat manaRegenRate;
    [Header("Ult stats")]
    public Stat maxUlt;

    public float currentHealth;
    public float currentMana;
    public float currentUlt;
    public float damageReceivedReductionPercentage = 0;

    public System.Action onHealthChanged;
    public event System.Action<float> OnDamageReceived;
    public event System.Action<float> onDamageDealt;
    protected bool isDead;
    public bool damaged;
 

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
        currentMana = GetMaxManaValue();
        damaged = false;
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        RegenerateMana();
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        float totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
        onDamageDealt?.Invoke(totalDamage);

    }
    #region Ult
    public virtual void DecreaseUlt(float amount)
    {
        currentUlt -= amount;

        currentUlt = Mathf.Max(currentUlt, 0f);
    }public virtual void IncreaseUlt(float amount)
    {
        currentUlt += amount;

        currentUlt = Mathf.Clamp(currentUlt, 0f, maxUlt.GetValue());
    }
    #endregion
    #region Mana
    public virtual void RegenerateMana()
    {
        currentMana += manaRegenRate.GetValue() * Time.deltaTime;

        // Убедимся, что значение маны не превышает максимальное
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana.GetValue());
    }
    public virtual void DecreaseMana(float amount)
    {
        currentMana -= amount;

        // Убедимся, что значение маны не станет меньше нуля
        currentMana = Mathf.Max(currentMana, 0f);
    }
    public virtual void IncreaseMana(float amount)
    {
        currentMana += amount;

        // Убедимся, что значение маны не превышает максимальное
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana.GetValue());
    }
    #endregion
    public virtual void TakeDamage(float _damage)
    {
        float reducedDamage = _damage * (1f - damageReceivedReductionPercentage / 100f);
        DecreaseHealthBy(reducedDamage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth < 0 && !isDead)
            Die();
        OnDamageReceived?.Invoke(_damage);

    }
    public virtual void TakeDamageWithoutKnockback(float _damage)
    {
        float reducedDamage = _damage * (1f - damageReceivedReductionPercentage / 100f);
        DecreaseHealthBy(reducedDamage);

        fx.StartCoroutine("FlashFX");

        if (currentHealth < 0 && !isDead)
            Die();
        OnDamageReceived?.Invoke(_damage);

    }

    protected virtual void DecreaseHealthBy(float _damage)
    {
        currentHealth -= _damage;
        damaged = true;
        if (onHealthChanged != null)
            onHealthChanged();
    }
    public virtual void IncreaseHealthBy(float healAmount)
    {
        if (healAmount <= 0)
        {
            Debug.LogWarning("Heal amount must be positive.");
            return;
        }

        currentHealth += healAmount;

        if (currentHealth > maxHealth.GetValue())
        {
            currentHealth = maxHealth.GetValue();
        }

        if (onHealthChanged != null)
        {
            onHealthChanged();
        }
    }


    protected virtual void Die()
    {
        isDead = true;
    }


    #region Stat calculations
    private float CheckTargetArmor(CharacterStats _targetStats, float totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue();


        totalDamage = Mathf.Clamp(totalDamage, 0, float.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        float totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }

    private bool CanCrit()
    {
        float totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }


        return false;
    }

    private float CalculateCriticalDamage(float _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public float GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
    public float GetMaxManaValue()
    {
        return maxMana.GetValue();
    }
    public float GetMaxUltValue()
    {
        return maxUlt.GetValue();
    }
    #endregion
}

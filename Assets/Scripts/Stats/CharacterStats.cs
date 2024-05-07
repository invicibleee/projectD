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
    public Stat critPower;              // default value 150%

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Mana stats")]
    public Stat maxMana;
    public Stat manaRegenRate;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;


    public bool isIgnited;   // does damage over time
    public bool isChilled;   // reduce armor by 20%
    public bool isShocked;   // reduce accuracy by 20%


    [SerializeField] private float ailmentsDuration = 4;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;


    private float igniteDamageCoodlown = .3f;
    private float igniteDamageTimer;
    private float igniteDamage;
    [SerializeField] private GameObject shockStrikePrefab;
    private float shockDamage;
    public float currentHealth;
    public float currentMana;
    public float damageReceivedReductionPercentage = 0;

    public System.Action onHealthChanged;
    public event System.Action<float> OnDamageReceived;
    public event System.Action<float> onDamageDealt;
    protected bool isDead;

 

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
        currentMana = GetMaxManaValue();

        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;


        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if (isIgnited)
            ApplyIgniteDamageEffect();

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
    #region Magical damage and ailemnts

    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        float _fireDamage = fireDamage.GetValue();
        float _iceDamage = iceDamage.GetValue();
        float _lightingDamage = lightingDamage.GetValue();



        float totalMagicalDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);
        _targetStats.TakeDamage(totalMagicalDamage);


        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
            return;


        AttemptyToApplyAilements(_targetStats, _fireDamage, _iceDamage, _lightingDamage);

    }

    private void AttemptyToApplyAilements(CharacterStats _targetStats, float _fireDamage, float _iceDamage, float _lightingDamage)
    {
        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .3f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                
                return;
            }

            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                
                return;
            }

            if (Random.value < .5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;

            }
        }

        if (canApplyIgnite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));

        if (canApplyShock)
            _targetStats.SetupShockStrikeDamage(Mathf.RoundToInt(_lightingDamage * .1f));

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }


    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChilled && !isShocked;
        bool canApplyChill = !isIgnited && !isChilled && !isShocked;
        bool canApplyShock = !isIgnited && !isChilled;


        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            ignitedTimer = ailmentsDuration;

            fx.IgniteFXFor(ailmentsDuration);
        }

        if (_chill && canApplyChill)
        {
            chilledTimer = ailmentsDuration;
            isChilled = _chill;

            float slowPercentage = .2f;

            GetComponent<Entity>().SlowEntityBy(slowPercentage, ailmentsDuration);
            fx.ChillFXFor(ailmentsDuration);
        }

        if (_shock && canApplyShock)
        {
            if (!isShocked)
            {
                ApplyShock(_shock);
            }
            else
            {
                if (GetComponent<Player>() != null)
                    return;

                HitNearestTargetWithShockStrike();
            }
        }

    }

    public void ApplyShock(bool _shock)
    {
        if (isShocked)
            return;

        shockedTimer = ailmentsDuration;
        isShocked = _shock;

        fx.ShockFXFor(ailmentsDuration);
    }

    private void HitNearestTargetWithShockStrike()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null && Vector2.Distance(transform.position, hit.transform.position) > 1)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }

            if (closestEnemy == null)            
                closestEnemy = transform;
        }


        if (closestEnemy != null)
        {
            GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);
            newShockStrike.GetComponent<ShockStrike_Controller>().Setup(shockDamage, closestEnemy.GetComponent<CharacterStats>());
        }
    }
    private void ApplyIgniteDamageEffect()
    {
        if (igniteDamageTimer < 0)
        {
            DecreaseHealthBy(igniteDamage);

            if (currentHealth < 0 && !isDead)
                Die();

            igniteDamageTimer = igniteDamageCoodlown;
        }
    }

    public void SetupIgniteDamage(float _damage) => igniteDamage = _damage;
    public void SetupShockStrikeDamage(float _damage) => shockDamage = _damage;

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

    protected virtual void DecreaseHealthBy(float _damage)
    {
        currentHealth -= _damage;

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
        if (_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        else
            totalDamage -= _targetStats.armor.GetValue();


        totalDamage = Mathf.Clamp(totalDamage, 0, float.MaxValue);
        return totalDamage;
    }


    private float CheckTargetResistance(CharacterStats _targetStats, float totalMagicalDamage)
    {
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, float.MaxValue);
        return totalMagicalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        float totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

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
    #endregion
}

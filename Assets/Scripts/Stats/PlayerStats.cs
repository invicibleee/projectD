using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    public BarsController allBars;
    private string saveKey = "playerSave";
    public bool isInvincible { get; private set; }

    protected override void Start()
    {
        base.Start();
        player= GetComponent<Player>();
        Load();
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
        if (isInvincible)
        {
            return;
        }
        allBars.SetHealth(currentHealth - _damage, GetMaxHealthValue());

    }
    protected override void Update()
    {
        base.Update();
        Save();
    }
    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;
    protected override void Die()
    {
        base.Die();
        player.Die();


    }
    public override void DoDamage(CharacterStats _targetStats)
    {
        base.DoDamage(_targetStats);
        IncreaseMana(5);
        IncreaseUlt(10);
        allBars.SetMana(currentMana, maxMana.GetValue());
        allBars.SetUlt(currentUlt, maxUlt.GetValue());
    }
    public override void RegenerateMana()
    {
        base.RegenerateMana();
        allBars.SetMana(currentMana, maxMana.GetValue());
    }
    public override void IncreaseHealthBy(float healAmount)
    {
        base.IncreaseHealthBy(healAmount);
        allBars.SetHealth(currentHealth, GetMaxHealthValue());
    }
    public override void DecreaseUlt(float amount)
    {
        base.DecreaseUlt(amount);
        allBars.SetUlt(currentUlt, GetMaxUltValue());
    }
    public override void DecreaseMana(float amount)
    {
        base.DecreaseMana(amount);
        allBars.SetMana(currentMana, GetMaxManaValue());
    }
     public bool TryUseMana(float amount)
    {
        if (currentMana >= amount)
        {
            DecreaseMana(amount);
            return true;
        }
        return false;
    }
    public void Save()
    {
        SaveManager.Save(saveKey, GetData());

    }


    private void Load()
    {
        var data = SaveManager.Load<SaveData.CharaStatistic>(saveKey);
        currentHealth = data._currentHealth;
        currentMana = data._currentMana;
        currentUlt = data._currentUlt;


    }

    private SaveData.CharaStatistic GetData()
    {
        var data = new SaveData.CharaStatistic()
        {
            _currentUlt = currentUlt,
            _currentMana = currentMana,
            _currentHealth = currentHealth,
        };
        return data;
    }
}

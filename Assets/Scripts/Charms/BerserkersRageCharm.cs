using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BerserkersRageCharm : Charm
{
    [SerializeField] private float damageIncreasePercentage;

    private float originalDamage;
    private float damageAdded;
    private bool effectApplied = false;
    private bool isActive = false;

    private void Update()
    {
        // ���������, ������� �� � ������ ������ ����
        if (isActive)
        {
            // �������� ������� ������ ���� ���� �������
            if (playerStats.currentHealth < playerStats.maxHealth.GetValue() && !effectApplied)
            {
                // ��������� ������ �����
                CharmEffect();
            }
            else if (playerStats.currentHealth >= playerStats.maxHealth.GetValue() && effectApplied)
            {
                // ������� ����������� �����, ���� �������� ������������� �� ������������� � ������ ��� ��������
                playerStats.damage.RemoveModifier(damageAdded);
                effectApplied = false;
            }
        }
    }

    public override void ActivateEffect()
    {
        originalDamage = playerStats.damage.GetValue();
        CharmEffect();
        // ��������� �������� �������� ����� ����� ����������� �������
        
        isActive = true;
    }

    public override void DeactivateEffect()
    {
        if (effectApplied)
        {
            playerStats.damage.RemoveModifier(damageAdded);
            effectApplied = false;
        }

        // ������������� ���� ���������� �����
        isActive = false;
    }

    public override void CharmEffect()
    {
        damageAdded = originalDamage * (damageIncreasePercentage / 100f);
        // ��������� ����������� �����
        playerStats.damage.AddModifier(damageAdded);
        // ������������� ����, ��� ������ ��������
        effectApplied = true;
    }
}

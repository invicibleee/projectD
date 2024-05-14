using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrophageHeartCharm : Charm
{
    [SerializeField] private float essenceDropIncreasePercentage = 0.2f; // ��������, 20%

    public override void CharmEffect()
    {
        // ������������� �� ������� ������ �����
        EnemyStats.OnEnemyDeath += IncreaseEssenceDrop;
    }
    public override void ActivateEffect()
    {
        base.ActivateEffect();
    }
    public override void DeactivateEffect()
    {
        // ������������ �� ������� ������ ����� ��� ����������� �����
        EnemyStats.OnEnemyDeath -= IncreaseEssenceDrop;
    }

    // �����, ������� ����� ���������� ��� ������ �����
    private void IncreaseEssenceDrop(EnemyStats enemyStats)
    {
        // ����������� ���������� ����������� �������� �� ������� �� �����
        enemyStats.essenceDropAmount = Mathf.RoundToInt(enemyStats.essenceDropAmount + (enemyStats.essenceDropAmount * essenceDropIncreasePercentage));
    }
}

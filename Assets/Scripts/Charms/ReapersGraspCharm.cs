using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReapersGraspCharm : Charm
{
    public float meleeAttackRangeIncreasePercentage;

    private float originalMeleeAttackRange;

    public override void ActivateEffect()
    {
        // ��������� �������� �������� ��������� ����� ����� ����������� �������
        originalMeleeAttackRange = playerStats.GetComponent<Entity>().GetAttackCheckRadius();

        // ��������� ������ �����
        base.ActivateEffect();
    }

    public override void DeactivateEffect()
    {
        // ���������� �������� ��������� ����� � ���������
        playerStats.GetComponent<Entity>().SetAttackCheckRadius(originalMeleeAttackRange);

        // �������� ����� �������� ������ ��� ����������� �����
        base.DeactivateEffect();
    }

    public override void CharmEffect()
{
    float currentRadius = playerStats.GetComponent<Entity>().GetAttackCheckRadius();
    float newRadius = currentRadius * (1 + meleeAttackRangeIncreasePercentage / 100f);
    playerStats.GetComponent<Entity>().SetAttackCheckRadius(newRadius);
}

}

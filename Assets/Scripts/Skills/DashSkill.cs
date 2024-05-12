using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    private float originalCooldown; // ������������ �������� ��������

    protected override void Start()
    {
        base.Start();
        originalCooldown = cooldown; // ��������� ������������ �������� ��������
    }

    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("Created clone behind");
    }

    // ����� ��� ��������� �������� ������
    public void ReduceCooldown(float reductionAmount)
    {
        cooldown = originalCooldown - reductionAmount; // ��������� �������
    }
    public void SetDefaultCooldown()
    {
        cooldown = originalCooldown; 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoSkill : Skill
{
    [SerializeField] private float cloneCooldown;
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float chronoDuration;
    [Space]
    [SerializeField] private GameObject chronoPrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;


    ChronofistSkillController currentChrono;
    protected override void Start()
    {
        base.Start();
    }
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newChrono = Instantiate(chronoPrefab, PlayerManager.instance.player.transform.position, Quaternion.identity);/////////////////////////////////

        currentChrono = newChrono.GetComponent<ChronofistSkillController>();

        currentChrono.SetupChrono(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown, chronoDuration);
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool SkillCompleted()
    {
        if (!currentChrono)
            return false;

        if (currentChrono.playerCanExitState)
        {
            currentChrono = null;
            return true;
        }

        return false;
    }
}

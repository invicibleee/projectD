using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceAbsorberCharm : Charm
{
    [SerializeField] private float essenceAbsorbPercentage; // Например, 10% энергии души
    private bool isActive;
    
    public override void CharmEffect()
    {
        if(isActive)
            EnemyStats.OnEnemyDeath += AbsorbEnemyEssence;
    }
    public override void ActivateEffect()
    {
        isActive = true;
        base.ActivateEffect();
    }
    public override void DeactivateEffect()
    {
        base.DeactivateEffect();
        isActive = false; 
        EnemyStats.OnEnemyDeath -= AbsorbEnemyEssence;
    }

    // Метод, который будет вызываться при смерти врага
    private void AbsorbEnemyEssence(EnemyStats enemyStats)
    {

        Debug.Log("Absorbed:" + playerStats.maxHealth.GetValue() * essenceAbsorbPercentage);
        playerStats.IncreaseHealthBy(playerStats.maxHealth.GetValue() * essenceAbsorbPercentage);
    }
}

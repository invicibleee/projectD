using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    protected D_DeathState stateData;

    public DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData)
        : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityEnemy(0f);
        // Start the coroutine to disable the enemy after deathTime
        enemy.StartCoroutine(DisableEnemyAfterDelay());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private IEnumerator DisableEnemyAfterDelay()
    {
        yield return new WaitForSeconds(stateData.deathTime);
        enemy.gameObject.SetActive(false);
        Debug.Log("Enemy is now inactive after delay.");
    }
}

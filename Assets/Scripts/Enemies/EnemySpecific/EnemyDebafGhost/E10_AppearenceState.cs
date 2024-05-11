using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_AppearenceState : AppearenceState
{
    private EnemyDebafGhost debafGhost;
    public E10_AppearenceState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName,D_AppearenceState stateData, EnemyDebafGhost debafGhost) : base(stateMashine, enemy, animBoolName,stateData)
    {
        this.debafGhost = debafGhost;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAppearenced) 
        { 
            if (isPlayerInMinArgoRange)
            {
                stateMashine.ChangeState(debafGhost.idleState);
            }
        }

    }   

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

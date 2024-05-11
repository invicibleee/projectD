using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_IdleState : IdleState
{
    private EnemyBossOne enemyBossOne;
    public B1_IdleState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_IdleState stateData, EnemyBossOne enemyBossOne) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.enemyBossOne = enemyBossOne;
    }

}

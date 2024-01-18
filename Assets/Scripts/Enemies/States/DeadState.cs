using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : EnemyState
{
    protected D_DeathState stateData;

    public DeadState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName) : base(stateMashine, enemy, animBoolName)
    {

    }
}

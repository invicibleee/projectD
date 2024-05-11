using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E10_DeathState : DeathState
{
    private EnemyDebafGhost debafGhost;
    public E10_DeathState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_DeathState stateData, EnemyDebafGhost debafGhost) : base(stateMashine, enemy, animBoolName, stateData)
    {
        this.debafGhost = debafGhost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(FiniteStateMashine stateMashine, Entity entity, string animBoolName) 
        : base(stateMashine, entity, animBoolName)
    {

    }
}

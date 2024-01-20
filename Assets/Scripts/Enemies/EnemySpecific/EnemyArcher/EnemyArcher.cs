using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    public E2_IdleState idleState {  get; private set; }

    public E2_MoveState moveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField] 
    private D_MoveState moveStateData;



    protected override void Start()
    {
        base.Start();
        moveState = new E2_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E2_IdleState(stateMashine, this, "idle", idleStateData, this);

        stateMashine.Initialize(moveState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

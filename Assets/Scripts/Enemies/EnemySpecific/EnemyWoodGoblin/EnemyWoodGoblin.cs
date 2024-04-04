using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyWoodGoblin : Enemy
{
    public E5_MoveState moveState { get; private set; }

    public E5_IdleState idleState { get; private set; }

    public E5_LookForPlayerState lookForPlayerState { get; private set; }

    public E5_PlayerDetectedState playerDetectedState { get; private set; }

    public E5_RangeAttackState rangeAttackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;
    [SerializeField]
    private D_RangeAttackState rangeAttackStateData;
    [SerializeField]
    private Transform rangeAttackPosition;
    protected override void Start()
    {
        base.Start();
        moveState = new E5_MoveState(stateMashine, this, "move", moveStateData, this);

        idleState = new E5_IdleState(stateMashine, this, "idle", idleStateData, this);

        playerDetectedState = new E5_PlayerDetectedState(stateMashine, this, "playerDetected", playerDetectedData, this);

        lookForPlayerState = new E5_LookForPlayerState(stateMashine, this, "lookForPlayer", lookForPlayerData, this);

        rangeAttackState = new E5_RangeAttackState(stateMashine, this, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);

        stateMashine.Initialize(moveState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
    }
}

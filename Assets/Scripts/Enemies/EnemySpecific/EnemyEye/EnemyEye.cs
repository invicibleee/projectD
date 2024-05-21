using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyEye : Enemy
{
    public E6_IdleState idleState {  get; private set; }

    public E6_LookForPlayer lookForPlayerState { get; private set; }

    public E6_MoveState moveState { get; private set; }

    public E6_PlayerDetected playerDetectedState { get; private set; }

    public E6_LazerAttackState lazerAttackState { get; private set; }

    public E6_DeathState deathState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_LazerAttackState lazerAttackStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private Transform lazerAttackPosition;
    [SerializeField]
    private D_DeathState deathStateData;

    protected override void Start()
    {
        base.Start();
        idleState = new E6_IdleState(stateMashine, this, "idle", idleStateData, this);

        moveState = new E6_MoveState(stateMashine,this,"move",moveStateData, this);

        playerDetectedState = new E6_PlayerDetected(stateMashine, this, "playerDetected", playerDetectedStateData, this);

        lazerAttackState = new E6_LazerAttackState(stateMashine, this, "rangeAttack", lazerAttackPosition, lazerAttackStateData, this);

        lookForPlayerState = new E6_LookForPlayer(stateMashine, this,"lookForPlayer",lookForPlayerStateData, this);

        deathState = new E6_DeathState(stateMashine,this,"death",deathStateData, this); 
        stateMashine.Initialize(moveState);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckDamageAndVisibility();
        if (stats.currentHealth <= 0)
        {
            stateMashine.ChangeState(deathState);
        }
    }

    private void CheckDamageAndVisibility()
    {
        if (stats.damaged && !CheckPlayerInMaxAgroRange())
        {
            stateMashine.ChangeState(lookForPlayerState);
            stats.damaged = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 1;
    protected Transform attackPosition;

    protected D_PlayerMeleeAttack stateData;

    protected AttackDetails attackDetails;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, Transform attackPosition, D_PlayerMeleeAttack stateData) : base(_player, _stateMachine, _animBoolName)
    {
        this.attackPosition = attackPosition;
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
       
        player.anim.SetInteger("ComboCounter", comboCounter);

        #region ChooseAttackDirection

        float attackDirection = player.facingDirection;

        if (xInput != 0)
            attackDirection = xInput;
        #endregion

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        
        player.StartCoroutine("BusyFor", .15f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsEnemy);

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }
}

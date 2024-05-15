using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //private bool hasAttacked;
 
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateCloneOnDashStart();

        stateTimer = player.dashDuration;
        //hasAttacked = false;
    }

    public override void Exit()
    {
        base.Exit();

        player.skill.clone.CreateCloneOnDashOver();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        player.SetVelocity(player.dashSpeed * player.dashDirection, 0);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

        //if (!hasAttacked)
        //{
        //    Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        //    foreach (var hit in colliders)
        //    {
        //        if (hit.GetComponent<Enemy>() != null)
        //        {
        //            EnemyStats _target = hit.GetComponent<EnemyStats>();
        //            player.stats.DoDamage(_target);
        //        }
        //    }

        //    hasAttacked = true;
        //}

        //if (stateTimer <= 0)
        //{
        //    hasAttacked = false;
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchScytheState : PlayerState
{
    private Transform scythe;
    public PlayerCatchScytheState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        scythe = player.scythe.transform;

        if (player.transform.position.x > scythe.position.x && player.facingDirection == 1)
            player.Flip();
        else if (player.transform.position.x < scythe.position.x && player.facingDirection == -1)
            player.Flip();

        rb.velocity = new Vector2(player.scytheReturnImpact * -player.facingDirection, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .1f);
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

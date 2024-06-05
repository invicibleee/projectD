using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimScytheState : PlayerState
{
    public PlayerAimScytheState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.scytheThrow.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();

        player.ZeroVelocity();

        if(Input.GetKeyUp(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_ult)))
        {
            stateMachine.ChangeState(player.idleState);
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDirection == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.facingDirection == -1)
            player.Flip();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            player.ResetJumps(); // Сброс прыжков при касании земли
        }
        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
        if (player.IsWallDetected() && TalantsPanelScript.instance.talants[1].isOwned)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (Input.GetKeyUp(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_jump)) && player.CanJump() && TalantsPanelScript.instance.talants[0].isOwned)
        {
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce); // Переход в состояние прыжка при нажатии клавиши пробел
            player.DecreaseJumpCount();
        }
    }
}

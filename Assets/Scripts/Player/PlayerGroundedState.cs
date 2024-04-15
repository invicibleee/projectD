using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        //if (Input.GetKeyDown(KeyCode.E))
        //    stateMachine.ChangeState(player.counterAttackState);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoScythe())
            stateMachine.ChangeState(player.aimScytheState);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttackState);

        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(player.chronoState);
    }

    private bool HasNoScythe()
    {
        if (!player.scythe) return true;

        player.scythe.GetComponent<ScytheSkillController>().ReturnScythe();
        return false;
    }
}

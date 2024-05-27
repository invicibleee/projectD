using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !ConversationManager.Instance.IsConversationActive)
            stateMachine.ChangeState(player.primaryAttackState);
        if (Input.GetKeyDown(KeyCode.R) && HasNoScythe() && AbilitiesPanelScript.instance.abilities[1].isEquiped)//ThrowSkill
            stateMachine.ChangeState(player.aimScytheState);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.R) && AbilitiesPanelScript.instance.abilities[0].isEquiped)// ChronoSKill
            stateMachine.ChangeState(player.chronoState);
    }

    private bool HasNoScythe()
    {
        if (!player.scythe) return true;

        player.scythe.GetComponent<ScytheSkillController>().ReturnScythe();
        return false;
    }
}
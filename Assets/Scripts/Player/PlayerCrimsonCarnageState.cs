using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrimsonCarnageState : PlayerState
{
    private RedWeaponStyle weaponStyle;
    public PlayerCrimsonCarnageState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, RedWeaponStyle _weaponStyle) : base(_player, _stateMachine, _animBoolName)
    {
        weaponStyle = _weaponStyle;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        weaponStyle.bleedDuration = weaponStyle.originalBleedDuration;
        weaponStyle.crimsonCarnage.ActivateCrimsonCarnage();
        weaponStyle.isBleeding = true;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}

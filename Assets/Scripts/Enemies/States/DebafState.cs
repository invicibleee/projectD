using UnityEngine;
public class DebafState : EnemyState
{
    protected D_DebafState stateData;
    protected Player player;
    protected bool isPlayerInMaxAgroRange;
    protected bool speedDebafApplied = false;
    protected float originalMoveSpeed;
    protected float debafMoveSpeed;

    public DebafState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, Player player, D_DebafState stateData) : base(stateMashine, enemy, animBoolName)
    {
        this.player = player;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMaxAgroRange = enemy.CheckPlayerInMaxAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        if (Time.time <= startTime + stateData.debafDuration)
        {
            debafMoveSpeed = player.moveSpeed;
            player.moveSpeed -= (debafMoveSpeed * stateData.debafSpeedProcent / 100.0f);
            speedDebafApplied = true;
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.moveSpeed= debafMoveSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }



}

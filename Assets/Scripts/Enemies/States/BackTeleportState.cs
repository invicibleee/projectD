using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTeleportState : EnemyState
{
    protected D_BackTeleportState stateData;
    
    protected bool performCloseRangeAction;
    protected Player player;
    protected bool isPLayerInMaxArgroRange;
    protected bool isGrounded;
    protected bool isTeleportOver;
    
    public BackTeleportState(EnemyStateMashine stateMashine, Enemy enemy, string animBoolName, D_BackTeleportState stateData, Player player) : base(stateMashine, enemy, animBoolName)
    {
        this.stateData = stateData;
        this.player = player;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = enemy.CheckPlayerInCloseRangeAction();
        isPLayerInMaxArgroRange = enemy.CheckPlayerInMaxAgroRange();
        isGrounded = enemy.IsGroundDetected();
    }

    public override void Enter()
    {
        base.Enter();
        isTeleportOver = false;

        
    }




    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.teleportTime && isGrounded)
        {                     
            if (enemy != null && player != null)
            {               
                Vector3 playerPosition = player.transform.position;
                Vector3 playerDirection = player.transform.right;                
                Vector3 oppositeDirection = -playerDirection;               
                Vector3 enemyPosition = playerPosition + oppositeDirection * stateData.distanceBehindPlayer;                
                enemy.transform.position = enemyPosition;                
                isTeleportOver = true;
            }
            else
            {
                Debug.LogError("Enemy or player is not set.");
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}

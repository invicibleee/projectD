using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebafGhost : Enemy
{
    protected Player player;
    public E10_PlayerDetectedSate playerDetectedSate {  get; private set; }

    public E10_LookForPLayerState lookForPLayerState { get; private set; }

    public E10_DebafState debafState { get; private set; }

    public E10_DeathState deathState { get; private set; }

    public E10_IdleState idleState { get; private set; }

    public E10_AppearenceState appearence { get; private set; }

    public E10_ChillState chillState { get; private set; }

    [SerializeField]
    private D_DeathState deathStateData;
    [SerializeField]
    public D_DebafState debafStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData1;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_AppearenceState appearenceStateData;
    protected override void Start()
    {
        base.Start();

        player = FindAnyObjectByType<Player>();

        idleState = new E10_IdleState(stateMashine,this,"idle",idleStateData,this);

        chillState = new E10_ChillState(stateMashine,this,"chill", lookForPlayerData1, this);

        playerDetectedSate = new E10_PlayerDetectedSate(stateMashine,this,"playerDetected",playerDetectedData,this);

        lookForPLayerState = new E10_LookForPLayerState(stateMashine,this,"lookForPlayer",lookForPlayerData,this);

        debafState = new E10_DebafState(stateMashine,this,"attack",player,debafStateData,this);

        deathState = new E10_DeathState(stateMashine,this,"death",deathStateData,this);

        appearence = new E10_AppearenceState(stateMashine, this, "appearence",appearenceStateData, this);

        stateMashine.Initialize(chillState);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckDamageAndVisibility();
        if (stats.currentHealth <= 0)
        {
            stateMashine.ChangeState(deathState);
        }
    }

    private void CheckDamageAndVisibility()
    {
        if (stats.damaged && !CheckPlayerInMaxAgroRange())
        {
            stateMashine.ChangeState(lookForPLayerState);
            stats.damaged = false;
        }
    }
}

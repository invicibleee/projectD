using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;
    public bool isBusy { get; private set; }
    [Header("Move Info")]
    public float moveSpeed = 5f;
    public float jumpForce;
    public float scytheReturnImpact;
    private float defaultMoveSpeed;
    private float defaultDashSpeed;
    private float defaultJumpForce;
    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashX;
    public float dashY;
    public float dashOffset;

    public float dashDirection { get; private set; }

    public GameObject scythe { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState{ get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerChronoState chronoState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerAimScytheState aimScytheState { get; private set; }
    public PlayerCatchScytheState catchScytheState { get; private set; }   
    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        chronoState = new PlayerChronoState(this, stateMachine, "Jump");
        aimScytheState = new PlayerAimScytheState(this, stateMachine, "AimScythe");
        catchScytheState = new PlayerCatchScytheState(this, stateMachine, "CatchScythe");
        
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        CheckForDashInput();
    }
    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }
    public void AssingNewScythe(GameObject _newScythe)
    {
        scythe = _newScythe;
    }
    public void CatchTheScythe()
    {
        stateMachine.ChangeState(catchScytheState);
        Destroy(scythe);
    }
    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy= false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = facingDirection;

            stateMachine.ChangeState(dashState);

            CharmManager.instance.blinkstrikeAmulet.PerformBlinkstrike(this);
        }

    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}

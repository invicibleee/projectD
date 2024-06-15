using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    private RedWeaponStyle style;

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

    public float coyoteTime; // ����������������� ����� ����� (� ��������)
    private float coyoteTimeCounter; // ������� ������� ��� ����� �����

    public float jumpBufferTime;
    private float jumpBufferCounter;

    public int maxJumps = 2; // ������������ ���������� �������
    private int currentJumps; // ������� ���������� �������
    private PlayerStats playerStats;
    public float dashDirection { get; private set; }

    public GameObject scythe { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
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
    public PlayerCrimsonCarnageState carnageState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        style = FindAnyObjectByType<RedWeaponStyle>();

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
        carnageState = new PlayerCrimsonCarnageState(this, stateMachine, "CrimsonCarnage",style);
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        GetStats();
        stateMachine.Initialize(idleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;

        currentJumps = maxJumps;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        // ������ ��� ��� �������� �������
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        // ��������� ������� "����� �����" ��� ������� ������
        if (Input.GetKey(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_ability)) && style.isBaseActive && stats.currentUlt == stats.maxUlt.GetValue())
        {
            stateMachine.ChangeState(carnageState);
        }
        if (IsGroundDetected())
        {
            coyoteTimeCounter = coyoteTime; // ������������� ������� ����� ����� ��� ������� ������ ������
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // ��������� ������� ����� �����
        }
        if (Input.GetKeyDown(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_jump)))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
        {
            stateMachine.ChangeState(jumpState);

            jumpBufferCounter = 0;
        }
        // ��������� ����������� ������ �� ����� ����� �����
        if (Input.GetKeyUp(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_jump)) && rb.velocity.y > 0)
        {
            coyoteTimeCounter = 0;
        }

        CheckForDashInput();
    }
    public void ResetJumps()
    {
        currentJumps = maxJumps;
    }
    public bool CanJump()
    {
        return currentJumps > 0;
    }

    public void DecreaseJumpCount()
    {
        currentJumps--;
    }
    public PlayerStats GetStats()
    {
        return playerStats = GetComponent<PlayerStats>();
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
        isBusy = false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(InputSettings.Instance.GetKeyForAction(InputSettings.Instance.Button_dash)) && SkillManager.instance.dash.CanUseSkill())
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




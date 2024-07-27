using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerState;

public class PlayerController : MonoBehaviour
{
	[Header("Event")]
	[SerializeField] UnityEvent onJumpEvent;
	public UnityEvent OnJumpEvent { get { return onJumpEvent; } set { onJumpEvent = value; } }
	[SerializeField] UnityEvent onFallEvent;
	public UnityEvent OnFallEvent { get { return onFallEvent; } set { onFallEvent = value; } }

	[Header("Components")]
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Renderer { get { return render; } }
	[SerializeField] Rigidbody2D rigid;
	public Rigidbody2D Rigid { get { return rigid; } }
	[SerializeField] LayerMask groundCheckLayer;

	[Header("Specs")]
	[SerializeField] int damage;
	public int Damage { get { return damage; } }
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float currentJumpPower;
	public float CurrentJumpPower { get { return currentJumpPower; } }
	[SerializeField] float jumpPowerRate;
	public float JumpPowerRate { get { return jumpPowerRate; } }
	[SerializeField] float jumpPowerMin;
	public float JumpPowerMin { get { return jumpPowerMin; } }
	[SerializeField] float jumpPowerMax;
	public float JumpPowerMax { get { return jumpPowerMax; } }
	[SerializeField] float lookTime;
	public float LookTime { get { return lookTime; } set { lookTime = value; } }
	[SerializeField] float lookTimeMax;
	public float LookTimeMax { get { return lookTimeMax; } set { lookTimeMax = value; } }
	[SerializeField] float attackCoolTime;
	public float AttackCoolTime { get { return attackCoolTime; } set { attackCoolTime = value; } }
	[SerializeField] float attackCount;
	public float AttackCount { get { return attackCount; } set { attackCount = value; } }
	[SerializeField] PlayerAttack playerAttack;

	[Header("Debug")]
	private Vector2 moveDir;
	public Vector2 MoveDir { get { return moveDir; } }
	private Vector2 lastMoveDir;
	public Vector2 LastMoveDir { get { return lastMoveDir; } }
	StateMachine<PlayerStateType> playerState;
	private bool isGround;
	public bool IsGround { get { return isGround; } }
	private bool isJumpCharging;
	public bool IsJumpCharging { get { return isJumpCharging; } }
	private bool isLookUp;
	public bool IsLookUp { get { return isLookUp; } }
	private bool isLookDown;
	public bool IsLookDown { get { return isLookDown; } }
	private bool isAttack;
	public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
	private bool isComboAttackActive;
	public bool IsComboAttackActive { get { return isComboAttackActive; } set { isComboAttackActive = value; } }
	private Coroutine lookRoutine;
	public Coroutine LookRoutine { get { return lookRoutine; } set { lookRoutine = value; } }
	private Coroutine jumpRoutine;
	public Coroutine JumpRoutine { get { return jumpRoutine; } set { jumpRoutine = value; } }
	private Coroutine attackRoutine;
	public Coroutine AttackRoutine { get { return attackRoutine; } set { attackRoutine = value; } }
	public PlayerStateType CurrentState;

	private void Awake()
	{
		playerState = new StateMachine<PlayerStateType>();
		playerState.AddState(PlayerStateType.Idle, new PlayerIdleState(this));
		playerState.AddState(PlayerStateType.Attack, new PlayerAttackState(this));
		playerState.Start(PlayerStateType.Idle);
	}

	private void Update()
	{
		playerState.Update();
		CurrentState = playerState.GetCurrentState();
	}

	private void FixedUpdate()
	{
		playerState.FixedUpdate();
		Move();

		if (isJumpCharging)
		{
			Jump();
		}

		if (rigid.velocity.y < -0.01f && !isGround)
		{
			animator.SetTrigger("Fall");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = true;
			animator.SetTrigger("Land");
			onFallEvent?.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = false;
		}
	}

	private void OnMove(InputValue value)
	{
		moveDir = value.Get<Vector2>();
		bool isMoving = moveDir != Vector2.zero;
		animator.SetBool("Move", isMoving);

		if (isMoving)
		{
			lastMoveDir = moveDir;
			render.flipX = moveDir.x > 0;
		}
	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed && isGround)
		{
			animator.SetTrigger("Jump");
			isJumpCharging = true;
			if (jumpRoutine != null)
			{
				StopCoroutine(jumpRoutine);
			}
			jumpRoutine = StartCoroutine(JumpCoroutine());
			onJumpEvent?.Invoke();
		}
		else
		{
			isJumpCharging = false;
		}
	}

	private void OnAttack(InputValue value)
	{
		if (!isAttack)
		{
			isAttack = true;
			attackCount = 0;
		}
		else
		{
			attackCount++;
		}
	}

	private void OnLookUp(InputValue value)
	{
		isLookUp = value.isPressed;
	}

	private void OnLookDown(InputValue value)
	{
		isLookDown = value.isPressed;
	}

	private void Move()
	{
		Vector2 velocity = rigid.velocity;
		velocity.x = moveDir.x * moveSpeed;
		rigid.velocity = velocity;
	}

	private void Jump()
	{
		Vector2 velocity = rigid.velocity;
		velocity.y = currentJumpPower;
		rigid.velocity = velocity;
	}

	public void OnAttackAnimationEvent(string effectName)
	{
		playerAttack.OnAttackAnimationEvent(effectName, lastMoveDir.x > 0);
	}

	IEnumerator JumpCoroutine()
	{
		while (isJumpCharging)
		{
			currentJumpPower += jumpPowerRate * Time.deltaTime;
			if (currentJumpPower >= jumpPowerMax)
			{
				break;
			}
			yield return null;
		}
		isJumpCharging = false;
		currentJumpPower = jumpPowerMin;
		jumpRoutine = null;
	}
}
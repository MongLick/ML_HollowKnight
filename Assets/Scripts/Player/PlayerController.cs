using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerState;

public class PlayerController : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Renderer { get { return render; } }
	[SerializeField] Rigidbody2D rigid;
	public Rigidbody2D Rigid { get { return rigid; } }
	[SerializeField] LayerMask groundCheckLayer;

	[Header("Specs")]
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float currentJumpPower;
	public float CurrentJumpPower { get { return currentJumpPower; } set { currentJumpPower = value; } }
	[SerializeField] float jumpChargingRate;
	public float JumpCharginRate { get { return jumpChargingRate; } set { jumpChargingRate = value; } }
	[SerializeField] float jumpPowerMax;
	public float JumpPowerMax { get { return jumpPowerMax; } set { jumpPowerMax = value; } }
	[SerializeField] float jumpPowerMin;
	public float JumpPowerMin { get { return jumpPowerMin; } set { jumpPowerMin = value; } }
	[SerializeField] float lookTime;
	public float LookTime { get { return lookTime; } set { lookTime = value; } }
	[SerializeField] float lookTimeMax;
	public float LookTimeMax { get { return lookTimeMax; } set { lookTimeMax = value; } }

	[Header("Debug")]
	private Vector2 moveDir;
	public Vector2 MoveDir { get { return moveDir; } }
	private Vector2 lastMoveDir;
	public Vector2 LastMoveDir { get { return lastMoveDir; } set { lastMoveDir = value; } }
	StateMachine<PlayerStateType> playerState;
	private bool isGround;
	public bool IsGround { get { return isGround; } }
	private bool isJumpCharging;
	public bool IsJumpCharging { get { return isJumpCharging; } set { isJumpCharging = value; } }
	private bool isLookUp;
	public bool IsLookUp { get { return isLookUp; } }
	private bool isLookDown;
	public bool IsLookDown { get { return isLookDown; } }
	private Coroutine lookUpChargingRoutine;
	public Coroutine LookUpChargingRoutine { get { return lookUpChargingRoutine; } set { lookUpChargingRoutine = value; } }
	private Coroutine lookDownChargingRoutine;
	public Coroutine LookDownChargingRoutine { get { return lookDownChargingRoutine; } set { lookDownChargingRoutine = value; } }
	private Coroutine jumpChargingRoutine;
	public Coroutine JumpChargingRoutine { get { return jumpChargingRoutine; } set { jumpChargingRoutine = value; } }
	public PlayerStateType CurrentState;

	private void Awake()
	{
		playerState = new StateMachine<PlayerStateType>();

		playerState.AddState(PlayerStateType.Idle, new PlayerIdleState(this));
		playerState.AddState(PlayerStateType.Move, new PlayerMoveState(this));
		playerState.AddState(PlayerStateType.Jump, new PlayerJumpState(this));
		playerState.AddState(PlayerStateType.LookUp, new PlayerLookUpState(this));
		playerState.AddState(PlayerStateType.LookDown, new PlayerLookDownState(this));

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
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = true;
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
		if (moveDir != Vector2.zero)
		{
			playerState.ChangeState(PlayerStateType.Move);
		}
		else
		{
			playerState.ChangeState(PlayerStateType.Idle);
		}
	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed && isGround)
		{
			isJumpCharging = true;
		}
		else
		{
			isJumpCharging = false;
		}
	}

	private void OnLookUp(InputValue value)
	{
		if (value.isPressed)
		{
			isLookUp = true;
		}
		else
		{
			isLookUp = false;
		}
	}

	private void OnLookDown(InputValue value)
	{
		if (value.isPressed)
		{
			isLookDown = true;
		}
		else
		{
			isLookDown = false;
		}
	}
}

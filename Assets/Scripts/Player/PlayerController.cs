using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerState;

public class PlayerController : MonoBehaviour, IDamageable
{
	private Dictionary<Transform, Vector3> initialChildPositions = new Dictionary<Transform, Vector3>();

	[Header("UnityEvent")]
	[SerializeField] UnityEvent onJumpEvent;
	public UnityEvent OnJumpEvent { get { return onJumpEvent; } set { onJumpEvent = value; } }
	[SerializeField] UnityEvent onFallEvent;
	public UnityEvent OnFallEvent { get { return onFallEvent; } set { onFallEvent = value; } }
	[SerializeField] UnityEvent onTakeHitEvent;
	public UnityEvent OnTakeHitEvent { get { return onTakeHitEvent; } set { onTakeHitEvent = value; } }
	[SerializeField] UnityEvent onDieEvent;
	public UnityEvent OnDieEvent { get { return onDieEvent; } set { onDieEvent = value; } }
	[SerializeField] UnityEvent onDashEvent;
	public UnityEvent OnDashEvent { get { return onDashEvent; } set { onDashEvent = value; } }

	[Header("Components")]
	[SerializeField] Collider2D playerCollider;
	public Collider2D PlayerCollider { get { return playerCollider; } }
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Renderer { get { return render; } }
	[SerializeField] Rigidbody2D rigid;
	public Rigidbody2D Rigid { get { return rigid; } }
	[SerializeField] PlayerAttack playerAttack;
	[SerializeField] Transform playerTransform;
	[SerializeField] CinemachineVirtualCamera cameraUp;
	public CinemachineVirtualCamera CameraUp { get { return cameraUp; } set { cameraUp = value; } }
	[SerializeField] CinemachineVirtualCamera cameraDown;
	public CinemachineVirtualCamera CameraDown { get { return cameraDown; } set { cameraDown = value; } }
	[SerializeField] PhysicsMaterial2D basicMaterial;
	public PhysicsMaterial2D BasicMaterial { get { return basicMaterial; } }
	[SerializeField] PhysicsMaterial2D takeHitMaterial;
	public PhysicsMaterial2D TakeHitMaterial { get { return takeHitMaterial; } }
	[SerializeField] Collider2D[] colliders = new Collider2D[20];
	[SerializeField] StateMachine<PlayerStateType> playerState;
	public PlayerStateType CurrentState;
	[SerializeField] AnimatorStateInfo stateInfo;
	[SerializeField] LayerMask groundCheckLayer;
	[SerializeField] LayerMask monsterCheckLayer;

	[Header("Vector")]
	private Vector2 moveDir;
	public Vector2 MoveDir { get { return moveDir; } set { moveDir = value; } }
	private Vector2 lastMoveDir;
	public Vector2 LastMoveDir { get { return lastMoveDir; } set { lastMoveDir = value; } }
	private Vector2 lastAttackDirection;
	public Vector2 LastAttackDirection { get { return lastAttackDirection; } set { lastAttackDirection = value; } }
	private Vector2 velocity;
	public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

	[Header("Coroutine")]
	private Coroutine lookRoutine;
	public Coroutine LookRoutine { get { return lookRoutine; } set { lookRoutine = value; } }
	private Coroutine dashRoutine;
	public Coroutine DashRoutine { get { return dashRoutine; } set { dashRoutine = value; } }
	private Coroutine jumpRoutine;
	public Coroutine JumpRoutine { get { return jumpRoutine; } set { jumpRoutine = value; } }
	private Coroutine attackRoutine;
	public Coroutine AttackRoutine { get { return attackRoutine; } set { attackRoutine = value; } }
	private Coroutine knockbackRoutine;
	public Coroutine KnockbackRoutine { get { return knockbackRoutine; } set { knockbackRoutine = value; } }
	private Coroutine takeHitRoutine;
	public Coroutine TakeHitRoutine { get { return takeHitRoutine; } set { takeHitRoutine = value; } }
	private Coroutine dieRoutine;
	public Coroutine DieRoutine { get { return dieRoutine; } set { dieRoutine = value; } }

	[Header("Specs")]
	[SerializeField] int damage;
	public int Damage { get { return damage; } }
	[SerializeField] int blinkCount;
	public int BlinkCount { get { return blinkCount; } }
	[SerializeField] int dieTime;
	public int DieTime { get { return dieTime; } }
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float knockbackPower;
	public float KnockbackPower { get { return knockbackPower; } }
	[SerializeField] float knockbackTime;
	public float KnockbackTime { get { return knockbackTime; } }
	[SerializeField] float dashSpeed;
	public float DashSpeed { get { return dashSpeed; } }
	[SerializeField] float dashTime;
	public float DashTime { get { return dashTime; } }
	[SerializeField] float dashCoolTime;
	public float DashCoolTime { get { return dashCoolTime; } }
	[SerializeField] float dashCoolTimeMax;
	public float DashCoolTimeMax { get { return dashCoolTimeMax; } }
	[SerializeField] float currentJumpPower;
	public float CurrentJumpPower { get { return currentJumpPower; } }
	[SerializeField] float jumpPowerRate;
	public float JumpPowerRate { get { return jumpPowerRate; } }
	[SerializeField] float jumpPowerMin;
	public float JumpPowerMin { get { return jumpPowerMin; } }
	[SerializeField] float jumpPowerMax;
	public float JumpPowerMax { get { return jumpPowerMax; } }
	[SerializeField] float maxFallSpeed;
	public float MaxFallSpeed { get { return maxFallSpeed; } }
	[SerializeField] float lookTime;
	public float LookTime { get { return lookTime; } set { lookTime = value; } }
	[SerializeField] float lookTimeMax;
	public float LookTimeMax { get { return lookTimeMax; } set { lookTimeMax = value; } }
	[SerializeField] float attackCoolTime;
	public float AttackCoolTime { get { return attackCoolTime; } set { attackCoolTime = value; } }
	[SerializeField] float attackCount;
	public float AttackCount { get { return attackCount; } set { attackCount = value; } }
	[SerializeField] float blinkDuration;
	public float BlinkDuration { get { return blinkDuration; } }
	[SerializeField] float pushX;
	public float PushX { get { return pushX; } }
	[SerializeField] float pushY;
	public float PushY { get { return pushY; } }
	[SerializeField] float hitKnockbackPower;
	public float HitKnockbackPower { get { return hitKnockbackPower; } }
	[SerializeField] float range;
	public float Range { get { return range; } }
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
	private bool isInteraction;
	public bool IsInteraction { get { return isInteraction; } set { isInteraction = value; } }
	private bool isComboAttackActive;
	public bool IsComboAttackActive { get { return isComboAttackActive; } set { isComboAttackActive = value; } }
	private bool isTakeHit;
	public bool IsTakeHit { get { return isTakeHit; } set { isTakeHit = value; } }
	private bool isBlink;
	public bool IsBlink { get { return isBlink; } set { isBlink = value; } }
	private bool isDie;
	public bool IsDie { get { return isDie; } set { isDie = value; } }
	private bool isDash;
	public bool IsDash { get { return isDash; } set { isDash = value; } }
	private bool cannotDash;
	public bool CannotDash { get { return cannotDash; } set { cannotDash = value; } }
	private bool isMonsterAttack;
	public bool IsMonsterAttack { get { return isMonsterAttack; } set { isMonsterAttack = value; } }
	private bool applyKnockback;
	public bool ApplyKnockback { get { return applyKnockback; } set { applyKnockback = value; } }
	private bool applyUpKnockback;
	public bool ApplyUpKnockback { get { return applyUpKnockback; } set { applyUpKnockback = value; } }
	private bool hasPlayedMoveSound;

	private void Awake()
	{
		playerState = new StateMachine<PlayerStateType>();
		playerState.AddState(PlayerStateType.Idle, new PlayerIdleState(this));
		playerState.AddState(PlayerStateType.Attack, new PlayerAttackState(this));
		playerState.AddState(PlayerStateType.TakeHit, new PlayerTakeHitState(this));
		playerState.AddState(PlayerStateType.Die, new PlayerDieState(this));
		playerState.AddState(PlayerStateType.Dash, new PlayerDashState(this));
		playerState.Start(PlayerStateType.Idle);
		playerTransform = transform;
	}

	private void Start()
	{
		SaveInitialChildPositions();
	}

	private void Update()
	{
		playerState.Update();
		CurrentState = playerState.GetCurrentState();
		if (cannotDash)
		{
			dashCoolTime += Time.deltaTime;
			if (dashCoolTime >= dashCoolTimeMax)
			{
				cannotDash = false;
			}
		}

		if (!isGround)
		{
			hasPlayedMoveSound = false;
			Manager.Sound.StopMoveSFX(Manager.Sound.PlayerMove);
		}

		if (!isTakeHit && !hasPlayedMoveSound && moveDir != Vector2.zero && isGround)
		{
			Manager.Sound.PlayMoveSFX(Manager.Sound.PlayerMove);
			hasPlayedMoveSound = true;
		}
	}

	private void FixedUpdate()
	{
		if (isDie || isTakeHit)
		{
			return;
		}

		playerState.FixedUpdate();

		if (!isMonsterAttack)
		{
			Move();
		}

		if (isJumpCharging)
		{
			Jump();
		}

		velocity = rigid.velocity;

		if (velocity.y < -maxFallSpeed)
		{
			velocity.y = -maxFallSpeed;
			rigid.velocity = velocity;
		}
		stateInfo = animator.GetCurrentAnimatorStateInfo(0);

		if (rigid.velocity.y < -0.01f && !isGround)
		{
			if (!stateInfo.IsName("Fall"))
			{
				animator.SetTrigger("Fall");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = true;
			Manager.Sound.PlaySFX(Manager.Sound.PlayerLand);
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

	private void OnDisable()
	{
		if (Manager.Sound != null)
		{
			Manager.Sound.StopMoveSFX(Manager.Sound.PlayerMove);
		}
	}

	private void OnMove(InputValue value)
	{
		if (isDie && isTakeHit)
		{
			return;
		}

		moveDir = value.Get<Vector2>();
		bool isMoving = moveDir != Vector2.zero;
		animator.SetBool("Move", isMoving);

		if (isMoving)
		{
			if (!hasPlayedMoveSound && isGround && !isTakeHit)
			{
				Manager.Sound.PlayMoveSFX(Manager.Sound.PlayerMove);
				hasPlayedMoveSound = true;
			}
			lastMoveDir = moveDir;
			if (isDash)
			{
				return;
			}
			render.flipX = moveDir.x > 0;
		}
		else
		{
			hasPlayedMoveSound = false;
			Manager.Sound.StopMoveSFX(Manager.Sound.PlayerMove);
		}
	}

	private void OnDash(InputValue value)
	{
		if (isDash || isDie || cannotDash || isAttack || isBlink)
		{
			return;
		}
		else
		{
			Manager.Sound.PlaySFX(Manager.Sound.PlayerDash);
			isDash = true;
			dashCoolTime = 0;
		}
	}

	private void OnJump(InputValue value)
	{
		if (value.isPressed && isGround && !isDie && !isTakeHit)
		{
			Manager.Sound.PlayJumpSFX(Manager.Sound.PlayerJump);
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
		if (isDie && isTakeHit)
		{
			return;
		}

		if (!isAttack)
		{
			isAttack = true;
			attackCount = 0;
		}
		else
		{
			attackCount++;
		}

		lastAttackDirection = lastMoveDir;
	}

	private void OnLookUp(InputValue value)
	{
		if (isDie)
		{
			return;
		}

		isLookUp = value.isPressed;
	}

	private void OnLookDown(InputValue value)
	{
		if (isDie)
		{
			return;
		}

		isLookDown = value.isPressed;
	}

	private void OnInteract(InputValue value)
	{
		Interact();
	}

	private void Move()
	{
		if (!isDash)
		{
			Vector2 velocity = rigid.velocity;
			velocity.x = moveDir.x * moveSpeed;
			rigid.velocity = velocity;
		}
	}

	private void Jump()
	{
		Vector2 velocity = rigid.velocity;
		velocity.y = currentJumpPower;
		rigid.velocity = velocity;
	}

	private void Interact()
	{
		int size = Physics2D.OverlapCircleNonAlloc(transform.position, range, colliders);

		for (int i = 0; i < size; i++)
		{
			IInteractable interactable = colliders[i].GetComponent<IInteractable>();
			if (interactable != null)
			{
				interactable.Interact(this);
				break;
			}
		}
	}

	public void TakeDamage(int damage, Transform hitPosition)
	{
		if (!isBlink)
		{
			if (hasPlayedMoveSound)
			{
				Manager.Sound.StopMoveSFX(Manager.Sound.PlayerMove);
				hasPlayedMoveSound = false;
			}

			pushX = transform.position.x - hitPosition.transform.position.x;
			rigid.velocity = new Vector2(pushX * hitKnockbackPower, pushY).normalized * hitKnockbackPower;

			Manager.Sound.PlaySFX(Manager.Sound.PlayerTakeHit);
			hasPlayedMoveSound = false;
			isBlink = true;
			isTakeHit = true;
			Manager.Data.GameData.Health -= damage;
			if (Manager.Data.GameData.Health <= 0)
			{
				isDie = true;
			}
		}
	}

	public void SaveInitialChildPositions()
	{
		foreach (Transform child in playerTransform)
		{
			initialChildPositions[child] = child.localPosition;
		}
	}

	public void RespawnPlayer()
	{
		if (playerTransform != null)
		{
			playerTransform.position = Manager.Game.RespawnPoint.position;
			RestoreChildPositions();
		}
	}

	public void RestoreChildPositions()
	{
		foreach (var pair in initialChildPositions)
		{
			if (pair.Key != null)
			{
				pair.Key.localPosition = pair.Value;
			}
		}

		render.flipX = lastMoveDir.x > 0;
	}

	public void OnAttackAnimationEvent(string effectName)
	{
		playerAttack.OnAttackAnimationEvent(effectName, lastAttackDirection.x > 0);
	}

	private IEnumerator JumpCoroutine()
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
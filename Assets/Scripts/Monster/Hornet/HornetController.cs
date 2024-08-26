using UnityEngine;
using UnityEngine.Events;
using static HornetState;

public class HornetController : Monster
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onSpearThrowEvent;
	public UnityEvent OnSpearThrowEvent { get { return onSpearThrowEvent; } set { onSpearThrowEvent = value; } }
	[SerializeField] UnityEvent onLaunchEvent;
	public UnityEvent OnLaunchEvent { get { return onLaunchEvent; } set { onLaunchEvent = value; } }
	[SerializeField] UnityEvent onCircularEvent;
	public UnityEvent OnCircularEvent { get { return onCircularEvent; } set { onCircularEvent = value; } }

	[Header("Components")]
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Render { get { return render; } }
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] Collider2D hornetcollider;
	public Collider2D Hornetcollider { get { return hornetcollider; } set { hornetcollider = value; } }
	[SerializeField] StateMachine<HornetStateType> hornetState;
	public HornetStateType CurrentState;
	[SerializeField] LayerMask playerCheck;
	public LayerMask PlayerCheck { get { return playerCheck; } }
	[SerializeField] LayerMask groundCheckLayer;

	[Header("Vector")]
	private Vector2 backStepDirection;
	public Vector2 BackStepDirection { get { return backStepDirection; } set { backStepDirection = value; } }
	private Vector2 dashDirection;
	public Vector2 DashDirection { get { return dashDirection; } set { dashDirection = value; } }
	private Vector2 moveDirection;
	public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
	private Vector2 velocity;

	[Header("Specs")]
	[SerializeField] int damage;
	public int Damage { get { return damage; } set { damage = value; } }
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float dashSpeed;
	public float DashSpeed { get { return dashSpeed; } }
	[SerializeField] float jumpPower;
	public float JumpPower { get { return jumpPower; } }
	[SerializeField] float maxFallSpeed;
	public float MaxFallSpeed { get { return maxFallSpeed; } }
	[SerializeField] float backStepPower;
	public float BackStepPower { get { return backStepPower; } }
	[SerializeField] float idleTime;
	public float IdleTime { get { return idleTime; } }
	[SerializeField] float moveTime;
	public float MoveTime { get { return moveTime; } }
	[SerializeField] float jumpTime;
	public float JumpTime { get { return jumpTime; } }
	[SerializeField] float fallTime;
	public float FallTime { get { return fallTime; } }
	[SerializeField] float dashTime;
	public float DashTime { get { return dashTime; } }
	[SerializeField] float dieTime;
	public float DieTime { get { return dieTime; } }
	[SerializeField] float spearThrowTime;
	public float SpearThrowTime { get { return spearThrowTime; } }
	[SerializeField] float circularAttackTime;
	public float CircularAttackTime { get { return circularAttackTime; } }
	[SerializeField] float groggyTime;
	public float GroggyTime { get { return groggyTime; } }
	[SerializeField] float backStepTime;
	public float BackStepTime { get { return backStepTime; } }
	private bool isTakeHit;
	public bool IsTakeHit { get { return isTakeHit; } set { isTakeHit = value; } }
	private bool isDie;
	public bool IsDie { get { return isDie; } set { isDie = value; } }
	private bool isGround;

	private void Awake()
	{
		hornetState = new StateMachine<HornetStateType>();
		hornetState.AddState(HornetStateType.Idle, new HornetIdleState(this));
		hornetState.AddState(HornetStateType.Move, new HornetMoveState(this));
		hornetState.AddState(HornetStateType.Jump, new HornetJumpState(this));
		hornetState.AddState(HornetStateType.Dash, new HornetDashState(this));
		hornetState.AddState(HornetStateType.SpearThrow, new HornetSpearThrowState(this));
		hornetState.AddState(HornetStateType.BackStep, new HornetBackStepState(this));
		hornetState.AddState(HornetStateType.CircularAttack, new HornetCircularAttackState(this));
		hornetState.AddState(HornetStateType.Groggy, new HornetGroggyState(this));
		hornetState.AddState(HornetStateType.Die, new HornetDieState(this));
		hornetState.Start(HornetStateType.Idle);
	}

	private void Update()
	{
		if (isDie)
		{
			return;
		}
		if (Manager.Game.Player.IsDie)
		{
			hornetState.ChangeState(HornetStateType.Idle);
		}
		CurrentState = hornetState.GetCurrentState();
		hornetState.Update();
		if (isGround)
		{
			if (Hp <= 0 && !isDie)
			{
				isDie = true;
				StopAllCoroutines();
				hornetState.ChangeState(HornetStateType.Die);
			}
		}
		if (Hp == 10 || Hp == 30 && !(hornetState.GetCurrentState() == HornetStateType.Groggy))
		{
			Hp--;
			StopAllCoroutines();
			hornetState.ChangeState(HornetStateType.Groggy);
		}
	}

	private void FixedUpdate()
	{
		if (isDie)
		{
			return;
		}

		hornetState.FixedUpdate();
		velocity = Rigid.velocity;

		if (Rigid.velocity.y < -0.01f && !isGround)
		{
			animator.SetTrigger("Fall");
		}

		if (velocity.y < -maxFallSpeed)
		{
			velocity.y = -maxFallSpeed;
			Rigid.velocity = velocity;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (playerCheck.Contain(collision.collider.gameObject.layer))
		{
			IDamageable damageable = collision.collider.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage);
				Rigid.velocity = Vector2.zero;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isDie)
		{
			return;
		}
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			Manager.Sound.PlaySFX(Manager.Sound.HornetLand);
			isGround = true;
			animator.SetTrigger("Land");
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			isGround = false;
		}
	}

	public override void ApplyKnockback(Vector2 direction)
	{

	}
}

using UnityEngine;
using static VengeflyState;

public class VengeflyController : Monster
{
	[Header("Components")]
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Render { get { return render; } }
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] Collider2D[] players;
	public Collider2D[] Players { get { return players; } set { players = value; } }
	[SerializeField] StateMachine<VengeflyStateType> vengeflyState;
	public VengeflyStateType CurrentState;
	[SerializeField] LayerMask playerCheck;
	public LayerMask PlayerCheck { get { return playerCheck; } }
	[SerializeField] LayerMask groundCheck;
	public LayerMask GroundCheck { get { return groundCheck; } }

	[Header("Vector")]
	[SerializeField] Vector2 moveDirection;
	public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
	[SerializeField] Vector2 startPos;
	public Vector3 StartPos { get { return startPos; } set { startPos = value; } }

	[Header("Coroutine")]
	private Coroutine takeHitRoutine;
	public Coroutine TakeHitRoutine { get { return takeHitRoutine; } set { takeHitRoutine = value; } }
	private Coroutine dieRoutine;
	public Coroutine DieRoutine { get { return dieRoutine; } set { dieRoutine = value; } }

	[Header("Specs")]
	[SerializeField] int damage;
	public int Damage { get { return damage; } set { damage = value; } }
	[SerializeField] float detectionRadius;
	public float DetectionRadius { get { return detectionRadius; } }
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float dieTime;
	public float DieTime { get { return dieTime; } }
	[SerializeField] float takeHitTime;
	public float TakeHitTime { get { return takeHitTime; } }
	[SerializeField] float yOffset;
	public float YOffset { get { return yOffset; } }
	[SerializeField] float minHeightFromGround;
	public float MinHeightFromGround { get { return minHeightFromGround; } }
	private bool isPlayerInRange;
	public bool IsPlayerInRange { get { return isPlayerInRange; } set { isPlayerInRange = value; } }
	private bool isTakeHit;
	public bool IsTakeHit { get { return isTakeHit; } set { isTakeHit = value; } }
	private bool isDie;
	public bool IsDie { get { return isDie; } set { isDie = value; } }

	private void Awake()
	{
		vengeflyState = new StateMachine<VengeflyStateType>();
		vengeflyState.AddState(VengeflyStateType.Idle, new VengeflyIdleState(this));
		vengeflyState.AddState(VengeflyStateType.Move, new VengeflyMoveState(this));
		vengeflyState.AddState(VengeflyStateType.Return, new VengeflyReturnState(this));
		vengeflyState.AddState(VengeflyStateType.TakeHit, new VengeflyTakeHitState(this));
		vengeflyState.AddState(VengeflyStateType.Die, new VengeflyDieState(this));
		vengeflyState.Start(VengeflyStateType.Idle);
	}

	private void Start()
	{
		startPos = transform.position;
	}

	private void Update()
	{
		CurrentState = vengeflyState.GetCurrentState();
		vengeflyState.Update();

		CheckForPlayer();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerCheck.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage, transform);
			}
		}

		if (collision.gameObject.CompareTag("AirGround"))
		{
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (playerCheck.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage, transform);
			}
		}
	}

	public override void TakeDamage(int damage, Transform hitPosition)
	{
		base.TakeDamage(damage, transform);
		vengeflyState.ChangeState(VengeflyStateType.TakeHit);
	}

	private void CheckForPlayer()
	{
		players = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerCheck);

		if(Manager.Game.Player.IsDie)
		{
			isPlayerInRange = false;
			Rigid.constraints |= RigidbodyConstraints2D.FreezePositionY;
			return;
		}

		if (players.Length > 0)
		{
			isPlayerInRange = true;
			Rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
		}
		else
		{
			isPlayerInRange = false;
			Rigid.constraints |= RigidbodyConstraints2D.FreezePositionY;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DetectionRadius);
	}
}

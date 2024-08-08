using UnityEngine;
using static VengeflyState;

public class VengeflyController : Monster, IDamageable
{
	[SerializeField] int hp;
	public int Hp { get { return hp; } set { hp = value; } }
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
	[SerializeField] bool isPlayerInRange;
	public bool IsPlayerInRange { get { return isPlayerInRange; } set { isPlayerInRange = value; } }
	private bool isTakeHit;
	public bool IsTakeHit { get { return isTakeHit; } set { isTakeHit = value; } }
	private bool isDie;
	public bool IsDie { get { return isDie; } set { isDie = value; } }
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Render { get { return render; } }
	[SerializeField] LayerMask playerCheck;
	public LayerMask PlayerCheck { get { return playerCheck; } }
	[SerializeField] Vector2 moveDirection;
	public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
	[SerializeField] Vector2 startPos;
	public Vector3 StartPos { get { return startPos; } set { startPos = value; } }
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] Collider2D[] players;
	public Collider2D[] Players { get { return players; } set { players = value; } }

	private Coroutine takeHitRoutine;
	public Coroutine TakeHitRoutine { get { return takeHitRoutine; } set { takeHitRoutine = value; } }
	private Coroutine dieRoutine;
	public Coroutine DieRoutine { get { return dieRoutine; } set { dieRoutine = value; } }

	StateMachine<VengeflyStateType> vengeflyState;
	public VengeflyStateType CurrentState;

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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (playerCheck.Contain(collision.collider.gameObject.layer))
		{
			IDamageable damageable = collision.collider.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage);
			}
		}
	}

	public void TakeDamage(int damage)
	{
		hp -= damage;
		vengeflyState.ChangeState(VengeflyStateType.TakeHit);
		Debug.Log(hp);
	}

	private void CheckForPlayer()
	{
		players = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerCheck);

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

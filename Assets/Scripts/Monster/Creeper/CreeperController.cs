using UnityEngine;
using static CreeperState;

public class CreeperController : MonoBehaviour, IDamageable
{
	[SerializeField] int hp;
	public int Hp { get { return hp; } set { hp = value; } }
	[SerializeField] int damage;
	public int Damage { get { return damage; } set { damage = value; } }
	[SerializeField] float detectionRadius;
	public float DetectionRadius { get { return detectionRadius; } }
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float checkDistance;
	public float CheckDistance { get { return checkDistance; } }
	[SerializeField] float dieTime;
	public float DieTime { get { return dieTime; } }
	[SerializeField] float takeHitTime;
	public float TakeHitTime { get { return takeHitTime; } }
	[SerializeField] bool isPlayerInRange;
	public bool IsPlayerInRange { get { return isPlayerInRange; } set { isPlayerInRange = value; } }
	[SerializeField] bool isMovingLeft;
	public bool IsMovingLeft { get { return isMovingLeft; } set { isMovingLeft = value; } }
	private bool isTakeHit;
	public bool IsTakeHit { get { return isTakeHit; } set { isTakeHit = value; } }
	private bool isDie;
	public bool IsDie { get { return isDie; } set { isDie = value; } }
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Render { get { return render; } }
	[SerializeField] LayerMask playerCheck;
	public LayerMask PlayerCheck { get { return playerCheck; } }
	[SerializeField] LayerMask obstacleCheck;
	public LayerMask ObstacleCheck { get { return obstacleCheck; } }
	[SerializeField] Vector2 moveDirection;
	public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }

	private Coroutine takeHitRoutine;
	public Coroutine TakeHitRoutine { get { return takeHitRoutine; } set { takeHitRoutine = value; } }
	private Coroutine dieRoutine;
	public Coroutine DieRoutine { get { return dieRoutine; } set { dieRoutine = value; } }

	StateMachine<CreeperStateType> creeperState;
	public CreeperStateType CurrentState;

	private void Awake()
	{
		creeperState = new StateMachine<CreeperStateType>();
		creeperState.AddState(CreeperStateType.Idle, new CreeperIdleState(this));
		creeperState.AddState(CreeperStateType.Move, new CreeperMoveState(this));
		creeperState.AddState(CreeperStateType.TakeHit, new CreeperTakeHitState(this));
		creeperState.AddState(CreeperStateType.Die, new CreeperDieState(this));
		creeperState.Start(CreeperStateType.Idle);
	}

	private void Update()
	{
		CurrentState = creeperState.GetCurrentState();
		creeperState.Update();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (playerCheck.Contain(collider.gameObject.layer))
		{
			IDamageable damageable = collider.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage);
			}
		}
	}

	public void TakeDamage(int damage)
	{
		hp -= damage;
		creeperState.ChangeState(CreeperStateType.TakeHit);
		Debug.Log(hp);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DetectionRadius);
	}
}

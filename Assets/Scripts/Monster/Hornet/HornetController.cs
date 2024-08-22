using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;
using static HornetState;

public class HornetController : Monster
{
	[SerializeField] int damage;
	public int Damage { get { return damage; } set { damage = value; } }
	[SerializeField] float moveSpeed;
	public float MoveSpeed { get { return moveSpeed; } }
	[SerializeField] float idleTime;
	public float IdleTime { get { return idleTime; } }
	[SerializeField] float moveTime;
	public float MoveTime { get { return moveTime; } }
	[SerializeField] float jumpTime;
	public float JumpTime { get { return jumpTime; } }
	[SerializeField] float dashTime;
	public float DashTime { get { return dashTime; } }
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
	private bool isMove;
	public bool IsMove { get { return isMove; } set { isMove = value; } }
	[SerializeField] SpriteRenderer render;
	public SpriteRenderer Render { get { return render; } }
	[SerializeField] LayerMask playerCheck;
	public LayerMask PlayerCheck { get { return playerCheck; } }
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	StateMachine<HornetStateType> hornetState;
	public HornetStateType CurrentState;
	[SerializeField] Vector2 moveDirection;
	public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }

	private void Awake()
	{
		hornetState = new StateMachine<HornetStateType>();
		hornetState.AddState(HornetStateType.Idle, new HornetIdleState(this));
		hornetState.AddState(HornetStateType.Move, new HornetMoveState(this));
		hornetState.AddState(HornetStateType.Jump, new HornetJumpState(this));
		hornetState.AddState(HornetStateType.Dash, new HornetDashState(this));
		hornetState.AddState(HornetStateType.SpearThrow, new HornetSpearThrowState(this));
		hornetState.AddState(HornetStateType.CircularAttack, new HornetCircularAttackState(this));
		hornetState.AddState(HornetStateType.Groggy, new HornetGroggyState(this));
		hornetState.AddState(HornetStateType.Die, new HornetDieState(this));
		hornetState.AddState(HornetStateType.BackStep, new HornetBackStepState(this));
		hornetState.Start(HornetStateType.Idle);
	}

	private void Update()
	{
		CurrentState = hornetState.GetCurrentState();
		hornetState.Update();
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

	public override void ApplyKnockback(Vector2 direction)
	{
		
	}
}

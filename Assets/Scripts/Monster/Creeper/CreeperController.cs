using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static CreeperState;

public class CreeperController : MonoBehaviour, IDamageable
{
	[SerializeField] int hp;
	[SerializeField] int damage = 1;
	[SerializeField] LayerMask playerCheck;
	[SerializeField] float detectionRadius;

	StateMachine<CreeperStateType> creeperState;
	bool isPlayerInRange;
	public bool IsPlayerInRange {get { return isPlayerInRange; } }
	public CreeperStateType CurrentState;

	private void Awake()
	{
		creeperState = new StateMachine<CreeperStateType>();
		creeperState.AddState(CreeperStateType.Idle, new CreeperMoveState(this));
		creeperState.AddState(CreeperStateType.Move, new CreeperMoveState(this));
		creeperState.AddState(CreeperStateType.TakeHit, new CreeperTakeHitState(this));
		creeperState.AddState(CreeperStateType.Die, new CreeperDieState(this));
		creeperState.Start(CreeperStateType.Idle);
	}

	private void Update()
	{
		CurrentState = creeperState.GetCurrentState();
		CheckForPlayer();

		if (isPlayerInRange)
		{
			creeperState.ChangeState(CreeperStateType.Move);
		}
	}

	private void CheckForPlayer()
	{
		Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerCheck);

		if (players != null)
		{
			isPlayerInRange = players.Length > 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerCheck.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();

			if (damageable != null)
			{
				damageable.TakeDamage(damage);
				creeperState.ChangeState(CreeperStateType.TakeHit);
			}
		}
	}

	public void TakeDamage(int damage)
	{
		hp -= damage;
		Debug.Log(hp);
		if (hp <= 0)
		{
			creeperState.ChangeState(CreeperStateType.Die);
			Destroy(gameObject);
		}
	}
}

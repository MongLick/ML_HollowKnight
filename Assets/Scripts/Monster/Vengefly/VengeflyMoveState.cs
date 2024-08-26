using UnityEngine;
using static VengeflyState;

public class VengeflyMoveState : BaseState<VengeflyStateType>
{
	private VengeflyController vengefly;

	public VengeflyMoveState(VengeflyController vengefly)
	{
		this.vengefly = vengefly;
	}

	public override void Update()
	{
		if (vengefly.IsPlayerInRange)
		{
			MoveTowardsPlayer();
		}
		else
		{
			ChangeState(VengeflyStateType.Return);
		}

		if (vengefly.IsTakeHit)
		{
			ChangeState(VengeflyStateType.TakeHit);
		}
	}

	private void MoveTowardsPlayer()
	{
		Transform playerTransform = vengefly.Players[0].transform;
		Vector3 direction = (playerTransform.position - vengefly.transform.position).normalized;
		Vector2 targetPosition = (Vector2)playerTransform.position;

		float distanceToPlayer = Vector2.Distance(vengefly.transform.position, targetPosition);
		float distanceThreshold = 0.1f;

		if (distanceToPlayer > distanceThreshold)
		{
			vengefly.Rigid.velocity = direction * vengefly.MoveSpeed;
		}
		else
		{
			vengefly.Rigid.velocity = Vector2.zero;
		}

		if (vengefly.Render != null)
		{
			vengefly.Render.flipX = direction.x < 0;
		}
	}
}

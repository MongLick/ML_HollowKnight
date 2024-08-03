using System.Collections;
using System.Collections.Generic;
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
		if (vengefly.Players.Length > 0)
		{
			Transform playerTransform = vengefly.Players[0].transform;
			Vector3 direction = (playerTransform.position - vengefly.transform.position).normalized;

			vengefly.MoveDirection = new Vector2(direction.x, direction.y);

			if (vengefly.Render != null)
			{
				vengefly.Render.flipX = direction.x < 0;
			}

			vengefly.transform.position += (Vector3)vengefly.MoveDirection * vengefly.MoveSpeed * Time.deltaTime;
		}
	}
}

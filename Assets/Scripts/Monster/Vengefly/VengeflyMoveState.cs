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
		Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + vengefly.YOffset, playerTransform.position.z);
		Vector3 direction = (targetPosition - vengefly.transform.position).normalized;

		float distanceToPlayer = Vector2.Distance(vengefly.transform.position, targetPosition);
		float distanceThreshold = 0.1f;

		RaycastHit2D hit = Physics2D.Raycast(vengefly.transform.position, Vector2.down, Mathf.Infinity, vengefly.GroundCheck);
		if (hit.collider != null && !hit.collider.CompareTag("AirGround"))
		{
			float distanceToGround = hit.distance;

			if (distanceToGround < vengefly.MinHeightFromGround)
			{
				vengefly.transform.position = new Vector2(vengefly.transform.position.x, vengefly.transform.position.y + (vengefly.MinHeightFromGround - distanceToGround));
			}
		}


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

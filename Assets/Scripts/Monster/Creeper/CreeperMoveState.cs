using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;

public class CreeperMoveState : BaseState<CreeperStateType>
{
	private CreeperController creeper;

	public CreeperMoveState(CreeperController creeper)
	{
		this.creeper = creeper;
	}

	public override void Enter()
	{
		if (creeper.MoveDirection == Vector2.zero)
		{
			creeper.MoveDirection = Vector2.left;
		}
	}

	public override void Update()
	{
		if (!IsObstacleInFront())
		{
			MoveForward();
		}
		else
		{
			TurnAround();
			MoveForward();
		}
	}

	private void MoveForward()
	{
		creeper.Rigid.velocity = creeper.MoveDirection * creeper.MoveSpeed;
	}

	private void TurnAround()
	{
		creeper.MoveDirection = creeper.MoveDirection == Vector2.left ? Vector2.right : Vector2.left;
		creeper.Render.flipX = creeper.MoveDirection != Vector2.left;
	}

	private bool IsObstacleInFront()
	{
		RaycastHit2D hit = Physics2D.Raycast(creeper.transform.position, creeper.MoveDirection, creeper.CheckDistance, creeper.ObstacleCheck);

		Debug.DrawRay(creeper.transform.position, (Vector3)creeper.MoveDirection * creeper.CheckDistance, Color.red);

		return hit.collider != null;
	}
}

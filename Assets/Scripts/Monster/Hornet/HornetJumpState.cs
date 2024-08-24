using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetJumpState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetJumpState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(JumpCoroutine());
	}

	public override void Update()
	{
		Vector2 playerPosition = Manager.Game.Player.transform.position;
		Vector2 currentPosition = hornet.transform.position;
		Vector2 playerDirection;

		playerDirection = new Vector2(playerPosition.x - currentPosition.x, 0).normalized;
		hornet.MoveDirection = playerDirection;
		if (playerDirection.x > 0)
		{
			hornet.Render.flipX = true;
		}
		else
		{
			hornet.Render.flipX = false;
		}
	}

	public override void FixedUpdate()
	{
		Jump();
	}

	private void Jump()
	{
		Vector2 jumpVelocity = new Vector2(hornet.Rigid.velocity.x, hornet.JumpPower);
		hornet.Rigid.velocity = jumpVelocity;
	}

	IEnumerator JumpCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetJump);
		hornet.Animator.SetTrigger("Jump");
		yield return new WaitForSeconds(hornet.JumpTime);
		int randomIndex = Random.Range(0, 2);
		if (randomIndex == 0)
		{
			ChangeState(HornetStateType.Idle);
		}
		else
		{
			ChangeState(HornetStateType.CircularAttack);
		}
	}
}

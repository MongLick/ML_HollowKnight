using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;
using static VengeflyState;

public class HornetMoveState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetMoveState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(MoveCoroutine());
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
		MoveForward();
	}

	private void MoveForward()
	{
		hornet.Rigid.velocity = hornet.MoveDirection * hornet.MoveSpeed;
	}

	IEnumerator MoveCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetMove);
		hornet.Animator.SetBool("Move", true);
		yield return new WaitForSeconds(hornet.MoveTime);
		hornet.Animator.SetBool("Move", false);
		ChangeState(HornetStateType.Idle);
	}
}

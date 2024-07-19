using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

public class PlayerMoveState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerMoveState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		player.Animator.SetBool("Move", true);
	}

	public override void Update()
	{
		if (player.IsJumpCharging && player.IsGround)
		{
			ChangeState(PlayerStateType.Jump);
		}

		if (player.MoveDir == Vector2.zero)
		{
			ChangeState(PlayerStateType.Idle);
		}

		player.LastMoveDir = player.MoveDir;

		if (player.MoveDir.x < 0)
		{
			player.Renderer.flipX = false;
		}
		else if (player.MoveDir.x > 0)
		{
			player.Renderer.flipX = true;
		}

		player.transform.Translate(player.MoveDir * player.MoveSpeed * Time.deltaTime);
	}

	public override void Exit()
	{
		player.Animator.SetBool("Move", false);
	}
}

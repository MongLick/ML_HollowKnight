using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

public class PlayerIdleState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerIdleState(PlayerController player)
	{
		this.player = player;
	}

	public override void Update()
	{
		if (player.MoveDir != Vector2.zero && !player.IsLookUp && !player.IsLookDown)
		{
			ChangeState(PlayerStateType.Move);
			return;
		}
		if(player.IsLookUp)
		{
			ChangeState(PlayerStateType.LookUp);
			return;
		}
		if(player.IsLookDown)
		{
			ChangeState(PlayerStateType.LookDown);
			return;
		}
		if (player.IsJumpCharging && player.IsGround)
		{
			ChangeState(PlayerStateType.Jump);
			return;
		}
	}
}

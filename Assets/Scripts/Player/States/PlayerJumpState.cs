using System.Collections;
using UnityEngine;
using static PlayerState;

public class PlayerJumpState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerJumpState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		player.JumpChargingRoutine = player.StartCoroutine(JumpChargingRoutine());
	}

	public override void Update()
	{
		if (player.MoveDir != Vector2.zero)
		{
			ChangeState(PlayerStateType.Move);
		}
		if (player.MoveDir == Vector2.zero && player.IsGround)
		{
			ChangeState(PlayerStateType.Idle);
		}
	}

	public override void Exit()
	{

	}

	private void Jump()
	{
		Vector2 velocity = player.Rigid.velocity;
		velocity.y = player.JumpPower;
		player.Rigid.velocity = velocity;
	}

	IEnumerator JumpChargingRoutine()
	{
		while (player.IsJumpCharging)
		{
			player.JumpPower += player.JumpCharginRate * Time.deltaTime;
			if (player.JumpPower >= player.JumpPowerMax)
			{
				player.JumpPower = player.JumpPowerMax;
				break;
			}
			yield return null;
		}

		Jump();
		player.JumpPower = player.JumpPowerMin;
	}
}

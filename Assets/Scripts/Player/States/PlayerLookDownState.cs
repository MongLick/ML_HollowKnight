using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

/*public class PlayerLookDownState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerLookDownState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		player.LookDownChargingRoutine = player.StartCoroutine(LookCooutine());
	}

	public override void Update()
	{
		if (!player.IsLookDown)
		{
			ChangeState(PlayerStateType.Idle);
		}
	}

	public override void Exit()
	{
		player.Animator.SetBool("LookDown", false);
		player.StopCoroutine(player.LookDownChargingRoutine);
		player.LookTime = 0;
	}

	IEnumerator LookCooutine()
	{
		while (player.IsLookDown)
		{
			player.LookTime += Time.deltaTime;

			if (player.IsLookDown && player.LookTime >= player.LookTimeMax)
			{
				player.Animator.SetBool("LookDown", true);
				break;
			}

			yield return null;
		}
	}
}*/

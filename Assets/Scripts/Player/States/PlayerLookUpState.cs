using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

/*public class PlayerLookUpState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerLookUpState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		player.LookUpChargingRoutine =  player.StartCoroutine(LookCooutine());
	}

	public override void Update()
	{
		if(!player.IsLookUp)
		{
			ChangeState(PlayerStateType.Idle);
		}
	}

	public override void Exit()
	{
		player.Animator.SetBool("LookUp", false);
		player.StopCoroutine(player.LookUpChargingRoutine);;
		player.LookTime = 0;
	}

	IEnumerator LookCooutine()
	{
		while (player.IsLookUp)
		{
			player.LookTime += Time.deltaTime;

			if (player.IsLookUp && player.LookTime >= player.LookTimeMax)
			{
				player.Animator.SetBool("LookUp", true);
				break;
			}

			yield return null;
		}
	}
}*/

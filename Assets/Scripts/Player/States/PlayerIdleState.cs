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
		if(player.IsLookUp || player.IsLookDown)
		{
			if(player.LookRoutine != null)
			{
				player.StopCoroutine(player.LookRoutine);
			}
			player.LookRoutine = player.StartCoroutine(LookCoroutine());
		}
		else
		{	
			player.Animator.SetBool("LookUp", false);
			player.Animator.SetBool("LookDown", false);
			player.LookTime = 0;
		}
		if(player.IsAttack)
		{
			ChangeState(PlayerStateType.Attack);
			return;
		}
	}

	IEnumerator LookCoroutine()
	{
		while (player.IsLookUp || player.IsLookDown)
		{
			player.LookTime += Time.deltaTime;

			if (player.LookTime >= player.LookTimeMax)
			{
				if(player.IsLookUp)
				{
					player.Animator.SetBool("LookDown", false);
					player.Animator.SetBool("LookUp", true);
				}
				else
				{
					player.Animator.SetBool("LookUp", false);
					player.Animator.SetBool("LookDown", true);
				}
				break;
			}

			yield return null;
		}

		player.LookRoutine = null;
	}
}

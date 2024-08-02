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

	public override void Enter()
	{
		player.Animator.SetBool("LookUp", false);
		player.Animator.SetBool("LookDown", false);
		player.LookTime = 0;
	}

	public override void Update()
	{
		if (player.IsTakeHit)
		{
			ChangeState(PlayerStateType.TakeHit);
		}
		else if (player.IsDash)
		{
			ChangeState(PlayerStateType.Dash);
		}

		if (player.IsAttack)
		{
			ChangeState(PlayerStateType.Attack);
			return;
		}

		if (player.IsLookUp || player.IsLookDown)
		{
			if (player.LookRoutine == null)
			{
				player.LookRoutine = player.StartCoroutine(LookCoroutine());
			}
		}
		else
		{
			if (player.LookRoutine != null)
			{
				player.StopCoroutine(player.LookRoutine);
				player.LookRoutine = null;
			}
			player.Animator.SetBool("LookUp", false);
			player.Animator.SetBool("LookDown", false);
			player.LookTime = 0;
		}
	}

	IEnumerator LookCoroutine()
	{
		while (player.IsLookUp || player.IsLookDown)
		{
			player.LookTime += Time.deltaTime;

			if (player.LookTime >= player.LookTimeMax)
            {
                player.Animator.SetBool("LookUp", player.IsLookUp);
                player.Animator.SetBool("LookDown", !player.IsLookUp);
                break;
            }

			yield return null;
		}

		player.LookRoutine = null;
	}
}
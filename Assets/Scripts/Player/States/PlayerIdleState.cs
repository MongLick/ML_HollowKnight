using System.Collections;
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
			player.CameraUp.gameObject.SetActive(false);
			player.CameraDown.gameObject.SetActive(false);
			player.LookTime = 0;
		}
	}

	private IEnumerator LookCoroutine()
	{
		while (player.IsLookUp || player.IsLookDown)
		{
			player.LookTime += Time.deltaTime;

			if (player.LookTime >= player.LookTimeMax)
			{
				player.Animator.SetBool("LookUp", player.IsLookUp);
				player.Animator.SetBool("LookDown", !player.IsLookUp);
			}
			if (player.LookTime >= 1)
			{
				if (player.IsLookUp)
				{
					player.CameraUp.gameObject.SetActive(true);
				}
				else
				{
					player.CameraUp.gameObject.SetActive(false);
				}
				if (player.IsLookDown)
				{
					player.CameraDown.gameObject.SetActive(true);
				}
				else
				{
					player.CameraDown.gameObject.SetActive(false);
				}
			}

			yield return null;
		}

		player.LookRoutine = null;
	}
}
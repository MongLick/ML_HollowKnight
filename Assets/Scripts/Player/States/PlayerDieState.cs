using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

public class PlayerDieState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerDieState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		player.Animator.SetTrigger("Die");
		if (player.DieRoutine != null)
		{
			player.StopCoroutine(player.DieRoutine);
		}
		player.DieRoutine = player.StartCoroutine(DieCoroutine());

		player.OnDieEvent?.Invoke();
	}

	IEnumerator DieCoroutine()
	{
		Manager.Scene.LoadScene("TitleScene");
		yield return new WaitForSeconds(player.DieTime);
	}
}

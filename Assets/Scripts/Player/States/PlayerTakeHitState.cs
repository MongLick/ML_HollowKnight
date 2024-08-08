using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerState;

public class PlayerTakeHitState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerTakeHitState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		foreach (var monsterCollider in GameManager.Instance.MonsterColliders)
		{
			Physics2D.IgnoreCollision(GameManager.Instance.PlayerCollider, monsterCollider, true);
		}

		if (player.TakeHitRoutine != null)
		{
			player.StopCoroutine(player.TakeHitRoutine);
		}
		player.TakeHitRoutine = player.StartCoroutine(TakeHitCoroutine());

		player.OnTakeHitEvent?.Invoke();
	}

	public override void Update()
	{
		if(!player.IsTakeHit)
		{
			if(player.IsDie)
			{
				ChangeState(PlayerStateType.Die);
			}
			else
			{
				ChangeState(PlayerStateType.Idle);
			}
		}
	}

	public override void Exit()
	{
		if (GameManager.Instance.PlayerCollider != null)
		{
			foreach (var monsterCollider in GameManager.Instance.MonsterColliders)
			{
				Physics2D.IgnoreCollision(GameManager.Instance.PlayerCollider, monsterCollider, false);
			}
		}
	}

	IEnumerator TakeHitCoroutine()
	{
		player.Animator.SetTrigger("TakeHit");

		for (int i = 0; i < player.BlinkCount; i++)
		{
			player.Renderer.color = Color.gray;
			yield return new WaitForSeconds(player.BlinkDuration);
			player.Renderer.color = Color.white;
			yield return new WaitForSeconds(player.BlinkDuration);
		}
		yield return new WaitForSeconds(player.BlinkDuration);
		player.IsTakeHit = false;
	}
}

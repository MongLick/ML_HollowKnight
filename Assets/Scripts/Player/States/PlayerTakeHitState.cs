using System.Collections;
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
		if (player.TakeHitRoutine != null)
		{
			player.StopCoroutine(player.TakeHitRoutine);
		}
		player.TakeHitRoutine = player.StartCoroutine(TakeHitCoroutine());

		player.OnTakeHitEvent?.Invoke();

		foreach (var monsterCollider in Manager.Game.MonsterColliders)
		{
			Physics2D.IgnoreCollision(Manager.Game.PlayerCollider, monsterCollider, true);
		}

		player.PlayerCollider.sharedMaterial = player.TakeHitMaterial;
	}

	public override void Update()
	{
		if (!player.IsBlink)
		{
			if (player.IsDie)
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
		foreach (var monsterCollider in Manager.Game.MonsterColliders)
		{
			Physics2D.IgnoreCollision(Manager.Game.PlayerCollider, monsterCollider, false);
		}
	}

	private IEnumerator TakeHitCoroutine()
	{
		player.Animator.SetTrigger("TakeHit");

		for (int i = 0; i < player.BlinkCount; i++)
		{
			player.Renderer.color = Color.gray;
			yield return new WaitForSeconds(player.BlinkDuration);
			player.Renderer.color = Color.white;
			yield return new WaitForSeconds(player.BlinkDuration);
		}
		if (player.IsDie)
		{
			player.IsBlink = false;
			yield break;
		}
		player.IsTakeHit = false;
		for (int i = 0; i < player.BlinkCount; i++)
		{
			player.Renderer.color = Color.gray;
			yield return new WaitForSeconds(player.BlinkDuration);
			player.Renderer.color = Color.white;
			yield return new WaitForSeconds(player.BlinkDuration);
		}
		player.PlayerCollider.sharedMaterial = player.BasicMaterial;
		player.IsBlink = false;
	}
}

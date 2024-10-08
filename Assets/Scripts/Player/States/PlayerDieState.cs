using System.Collections;
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
		Manager.Sound.PlaySFX(Manager.Sound.PlayerDie);
		player.PlayerCollider.enabled = false;
		player.Rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
		player.Animator.SetTrigger("Die");
		if (player.DieRoutine != null)
		{
			player.StopCoroutine(player.DieRoutine);
		}
		player.DieRoutine = player.StartCoroutine(DieCoroutine());
		player.OnDieEvent?.Invoke();
	}

	private IEnumerator DieCoroutine()
	{
		yield return new WaitForSeconds(player.DieTime);
	}
}

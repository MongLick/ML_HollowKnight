using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static PlayerState;

public class PlayerAttackState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerAttackState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		if (player.AttackRoutine != null)
		{
			player.StopCoroutine(player.AttackRoutine);
		}
		player.AttackRoutine = player.StartCoroutine(AttackCoroutine());
	}

	public override void Update()
	{
		if(!player.IsAttack)
		{
			ChangeState(PlayerStateType.Idle);
			return;
		}
	}

	public override void Exit()
	{
		player.IsAttack = false;
		player.AttackCount = 0;
	}

	public void OnAttackAnimationEnd()
	{
		if (player.AttackCount > 0)
		{
			player.Animator.SetTrigger("Attack2");
			player.AttackCount--;
		}
	}

	IEnumerator AttackCoroutine()
	{
		if (player.IsLookUp)
		{
			player.Animator.SetTrigger("AttackTop");
		}
		else if (player.IsLookDown && player.Rigid.velocity != Vector2.zero)
		{
			player.Animator.SetTrigger("AttackBottom");
		}
		else
		{
			player.Animator.SetTrigger("Attack1");
		}

		yield return new WaitForSeconds(player.AttackCoolTime);

		player.IsAttack = true;

		if (player.AttackCount > 0)
		{
			player.Animator.SetTrigger("Attack2");
			player.AttackCount--;
			yield return new WaitForSeconds(player.AttackCoolTime);
		}

		player.IsAttack = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
		if (player.IsTakeHit)
		{
			ChangeState(PlayerStateType.TakeHit);
		}
		if (player.AttackRoutine == null)
		{
			ChangeState(PlayerStateType.Idle);
		}
		else if (!player.IsAttack)
		{
			player.StopCoroutine(player.AttackRoutine);
			player.AttackRoutine = null;
			ChangeState(PlayerStateType.Idle);
		}
	}
		

	public override void Exit()
	{
		player.IsAttack = false;
		player.AttackCount = 0;
		player.AttackRoutine = null;
		player.IsComboAttackActive = false;
	}

	IEnumerator AttackCoroutine()
	{
		if (player.IsLookUp)
		{
			player.Animator.SetTrigger("AttackTop");
		}
		else if (player.IsLookDown && player.Rigid.velocity.y != 0)
		{
			player.Animator.SetTrigger("AttackBottom");
		}
		else
		{
			player.Animator.SetTrigger("Attack1");
			player.IsComboAttackActive = true;
		}

		yield return new WaitForSeconds(player.AttackCoolTime);

		if (player.IsComboAttackActive && player.AttackCount > 0)
		{
			player.Animator.SetTrigger("Attack2");
			player.AttackCount = 0;
			yield return new WaitForSeconds(player.AttackCoolTime);
			player.IsComboAttackActive = false;
		}

		player.IsAttack = false;
		player.AttackCount = 0;
		ChangeState(PlayerStateType.Idle);
	}
}

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

	public override void FixedUpdate()
	{
		if (player.IsMonsterAttack)
		{
			if (player.ApplyUpKnockback)
			{
				ApplyUpKnockback();
			}
			else if(player.ApplyKnockback)
			{
				ApplyKnockback();
			}
		}
	}

	public override void Exit()
	{
		player.IsMonsterAttack = false;
		player.IsAttack = false;
		player.AttackCount = 0;
		player.AttackRoutine = null;
		player.IsComboAttackActive = false;
		player.ApplyKnockback = false;
		player.ApplyUpKnockback = false;
	}

	private void ApplyKnockback()
	{
		Vector2 velocity = player.Rigid.velocity;

		Vector2 knockbackDirection = player.LastAttackDirection.x > 0 ? new Vector2(-1, 0) : new Vector2(1, 0);

		Vector2 newVelocity = new Vector2(knockbackDirection.x * player.KnockbackPower, velocity.y);

		player.Rigid.velocity = newVelocity;

		player.KnockbackRoutine = player.StartCoroutine(SmoothKnockbackCoroutine());
	}

	private void ApplyUpKnockback()
	{
		Vector2 velocity = player.Rigid.velocity;
		velocity.y = player.KnockbackPower * 2;
		player.Rigid.velocity = velocity;

		player.KnockbackRoutine = player.StartCoroutine(SmoothKnockbackCoroutine());
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
			player.ApplyUpKnockback = true;
		}
		else
		{
			player.Animator.SetTrigger("Attack1");
			player.IsComboAttackActive = true;
			player.ApplyKnockback = true;
		}

		yield return new WaitForSeconds(player.AttackCoolTime);

		if (player.IsComboAttackActive && player.AttackCount > 0)
		{
			player.Animator.SetTrigger("Attack2");
			player.ApplyKnockback = true;
			player.AttackCount = 0;
			yield return new WaitForSeconds(player.AttackCoolTime);
			player.IsComboAttackActive = false;
		}

		player.IsAttack = false;
		player.AttackCount = 0;
		player.IsMonsterAttack = false;
		ChangeState(PlayerStateType.Idle);
	}

	IEnumerator SmoothKnockbackCoroutine()
	{
		yield return new WaitForSeconds(player.KnockbackTime);
		player.ApplyKnockback = false;
		player.ApplyUpKnockback= false;
		player.IsMonsterAttack = false;
	}
}

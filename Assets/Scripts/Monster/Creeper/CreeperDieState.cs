using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;

public class CreeperDieState : BaseState<CreeperStateType>
{
	private CreeperController creeper;

	public CreeperDieState(CreeperController creeper)
	{
		this.creeper = creeper;
	}

	public override void Enter()
	{
		if (creeper.DieRoutine != null)
		{
			creeper.StopCoroutine(creeper.DieRoutine);
		}
		creeper.DieRoutine = creeper.StartCoroutine(DieCoroutine());
	}

	IEnumerator DieCoroutine()
	{
		creeper.Animator.SetTrigger("Die");
		yield return new WaitForSeconds(creeper.DieTime);
		Object.Destroy(creeper.gameObject);
	}
}

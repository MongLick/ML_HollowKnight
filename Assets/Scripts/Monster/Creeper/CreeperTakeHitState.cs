using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;

public class CreeperTakeHitState : BaseState<CreeperStateType>
{
	private CreeperController creeper;

	public CreeperTakeHitState(CreeperController creeper)
	{
		this.creeper = creeper;
	}

	public override void Enter()
	{
		creeper.IsTakeHit = true;

		if (creeper.TakeHitRoutine != null)
		{
			creeper.StopCoroutine(creeper.TakeHitRoutine);
		}
		creeper.TakeHitRoutine = creeper.StartCoroutine(TakeHitCoroutine());
	}

	public override void Update()
	{
		if(creeper.IsDie)
		{
			ChangeState(CreeperStateType.Die);
		}
		else if(!creeper.IsTakeHit)
		{
			ChangeState(CreeperStateType.Move);
		}
	}

	IEnumerator TakeHitCoroutine()
	{
		creeper.Animator.SetTrigger("TakeHit");
		yield return new WaitForSeconds(creeper.TakeHitTime);
		creeper.IsTakeHit = false;
		if(creeper.Hp <= 0)
		{
			creeper.IsDie = true;
		}
	}
}

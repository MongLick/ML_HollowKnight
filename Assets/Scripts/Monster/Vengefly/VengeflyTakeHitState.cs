using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VengeflyState;

public class VengeflyTakeHitState : BaseState<VengeflyStateType>
{
	private VengeflyController vengefly;

	public VengeflyTakeHitState(VengeflyController vengefly)
	{
		this.vengefly = vengefly;
	}

	public override void Enter()
	{
		vengefly.IsTakeHit = true;

		if (vengefly.TakeHitRoutine != null)
		{
			vengefly.StopCoroutine(vengefly.TakeHitRoutine);
		}
		vengefly.TakeHitRoutine = vengefly.StartCoroutine(TakeHitCoroutine());
	}

	public override void Update()
	{
		if (vengefly.IsDie)
		{
			ChangeState(VengeflyStateType.Die);
		}
		else if (!vengefly.IsTakeHit)
		{
			ChangeState(VengeflyStateType.Move);
		}
	}

	IEnumerator TakeHitCoroutine()
	{
		vengefly.Animator.SetTrigger("TakeHit");
		yield return new WaitForSeconds(vengefly.TakeHitTime);
		vengefly.IsTakeHit = false;
		if (vengefly.Hp <= 0)
		{
			vengefly.IsDie = true;
		}
	}
}

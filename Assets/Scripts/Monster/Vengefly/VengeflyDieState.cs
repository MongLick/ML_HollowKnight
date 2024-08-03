using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VengeflyState;

public class VengeflyDieState : BaseState<VengeflyStateType>
{
	private VengeflyController vengefly;

	public VengeflyDieState(VengeflyController vengefly)
	{
		this.vengefly = vengefly;
	}

	public override void Enter()
	{
		if (vengefly.DieRoutine != null)
		{
			vengefly.StopCoroutine(vengefly.DieRoutine);
		}
		vengefly.DieRoutine = vengefly.StartCoroutine(DieCoroutine());
	}

	IEnumerator DieCoroutine()
	{
		vengefly.Animator.SetTrigger("Die");
		yield return new WaitForSeconds(vengefly.DieTime);
		Object.Destroy(vengefly.gameObject);
	}
}

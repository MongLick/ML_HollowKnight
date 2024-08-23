using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetSpearThrowState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetSpearThrowState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(SpearThrowCoroutine());
	}

	public override void FixedUpdate()
	{
		SpearThrow();
	}

	private void SpearThrow()
	{
		
	}

	IEnumerator SpearThrowCoroutine()
	{
		hornet.Animator.SetTrigger("SpearThrow");
		yield return new WaitForSeconds(hornet.SpearThrowTime);
		ChangeState(HornetStateType.Idle);
	}
}

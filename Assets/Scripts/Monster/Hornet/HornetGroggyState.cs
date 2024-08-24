using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetGroggyState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetGroggyState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(GroggyCoroutine());
	}

	IEnumerator GroggyCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetGroggy);
		hornet.Animator.SetBool("Move", false);
		hornet.Animator.SetBool("Groggy", true);
		yield return new WaitForSeconds(hornet.GroggyTime);
		hornet.Animator.SetBool("Groggy", false);
		ChangeState(HornetStateType.Idle);
	}
}

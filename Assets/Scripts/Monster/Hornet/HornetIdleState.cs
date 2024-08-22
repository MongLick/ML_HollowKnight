using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;
using static PlayerState;
using static UnityEditor.VersionControl.Asset;

public class HornetIdleState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetIdleState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		int randomIndex = Random.Range(1, 9);
		HornetStateType randomState = (HornetStateType)randomIndex;
		hornet.StartCoroutine(IdleCoroutine());
		//ChangeState(randomState);
		ChangeState(HornetStateType.Move);
	}

	IEnumerator IdleCoroutine()
	{
		yield return new WaitForSeconds(hornet.IdleTime);
	}
}

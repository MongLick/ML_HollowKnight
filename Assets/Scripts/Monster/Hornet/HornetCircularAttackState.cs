using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetCircularAttackState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetCircularAttackState(HornetController hornet)
	{
		this.hornet = hornet;
	}
}

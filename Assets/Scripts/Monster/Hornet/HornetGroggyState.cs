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
}

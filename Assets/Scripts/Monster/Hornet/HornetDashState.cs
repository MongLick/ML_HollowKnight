using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetDashState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetDashState(HornetController hornet)
	{
		this.hornet = hornet;
	}
}

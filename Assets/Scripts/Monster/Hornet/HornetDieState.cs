using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetDieState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetDieState(HornetController hornet)
	{
		this.hornet = hornet;
	}
}

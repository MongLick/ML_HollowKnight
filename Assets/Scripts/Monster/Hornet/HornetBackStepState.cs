using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetBackStepState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetBackStepState(HornetController hornet)
	{
		this.hornet = hornet;
	}
}

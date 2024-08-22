using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetJumpState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetJumpState(HornetController hornet)
	{
		this.hornet = hornet;
	}
}

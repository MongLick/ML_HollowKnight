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
}

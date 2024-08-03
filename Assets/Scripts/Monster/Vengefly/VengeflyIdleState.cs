using UnityEngine;
using static VengeflyState;

public class VengeflyIdleState : BaseState<VengeflyStateType>
{
	private VengeflyController vengefly;

	public VengeflyIdleState(VengeflyController vengefly)
	{
		this.vengefly = vengefly;
	}

	public override void Update()
	{
		if (vengefly.IsPlayerInRange)
		{
			ChangeState(VengeflyStateType.Move);
		}
	}
}

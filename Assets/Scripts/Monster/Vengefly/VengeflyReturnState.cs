using UnityEngine;
using static VengeflyState;

public class VengeflyReturnState : BaseState<VengeflyStateType>
{
	private VengeflyController vengefly;

	public VengeflyReturnState(VengeflyController vengefly)
	{
		this.vengefly = vengefly;
	}

	public override void Update()
	{
		if (vengefly.IsPlayerInRange)
		{
			ChangeState(VengeflyStateType.Move);
		}

		Vector3 direction = (vengefly.StartPos - vengefly.transform.position).normalized;
		vengefly.transform.position += direction * vengefly.MoveSpeed * Time.deltaTime;

		if (vengefly.Render != null)
		{
			vengefly.Render.flipX = direction.x < 0;
		}

		if (Vector2.Distance(vengefly.transform.position, vengefly.StartPos) < 0.01f)
		{
			vengefly.transform.position = vengefly.StartPos;
			ChangeState(VengeflyStateType.Idle);
		}
	}
}

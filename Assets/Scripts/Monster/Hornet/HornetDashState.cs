using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetDashState : BaseState<HornetStateType>
{
	private HornetController hornet;
	private Vector2 dashDirection;

	public HornetDashState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		dashDirection = hornet.MoveDirection.normalized;
		hornet.StartCoroutine(DashCoroutine());
	}

	public override void FixedUpdate()
	{
		DashForward();
	}

	private void DashForward()
	{
		hornet.Rigid.velocity = dashDirection * hornet.DashSpeed;
	}

	IEnumerator DashCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetDash);
		hornet.OnLaunchEvent?.Invoke();
		hornet.Animator.SetTrigger("Dash");
		yield return new WaitForSeconds(hornet.DashTime);
		ChangeState(HornetStateType.Idle);
	}
}

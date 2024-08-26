using System.Collections;
using UnityEngine;
using static HornetState;

public class HornetDashState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetDashState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.DashDirection = hornet.MoveDirection.normalized;
		hornet.StartCoroutine(DashCoroutine());
	}

	public override void FixedUpdate()
	{
		DashForward();
	}

	private void DashForward()
	{
		hornet.Rigid.velocity = hornet.DashDirection * hornet.DashSpeed;
	}

	private IEnumerator DashCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetDash);
		hornet.OnLaunchEvent?.Invoke();
		hornet.Animator.SetTrigger("Dash");
		yield return new WaitForSeconds(hornet.DashTime);
		ChangeState(HornetStateType.Idle);
	}
}

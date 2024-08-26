using System.Collections;
using UnityEngine;
using static HornetState;

public class HornetSpearThrowState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetSpearThrowState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(SpearThrowCoroutine());
	}

	private IEnumerator SpearThrowCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetSpear);
		hornet.Animator.SetTrigger("SpearThrow");
		yield return new WaitForSeconds(0.2f);
		hornet.OnLaunchEvent?.Invoke();
		hornet.OnSpearThrowEvent?.Invoke();
		yield return new WaitForSeconds(hornet.SpearThrowTime);
		ChangeState(HornetStateType.Idle);
	}
}

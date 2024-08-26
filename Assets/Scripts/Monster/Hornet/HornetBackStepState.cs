using System.Collections;
using UnityEngine;
using static HornetState;

public class HornetBackStepState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetBackStepState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.BackStepDirection = hornet.MoveDirection.normalized;
		hornet.StartCoroutine(BackStepCoroutine());
	}

	public override void FixedUpdate()
	{
		MoveBackStep();
	}

	private void MoveBackStep()
	{
		hornet.Rigid.velocity = -hornet.BackStepDirection * hornet.BackStepPower;
	}

	IEnumerator BackStepCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetBackStep);
		hornet.Animator.SetTrigger("BackStep");
		yield return new WaitForSeconds(hornet.BackStepTime);
		ChangeState(HornetStateType.Idle);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetBackStepState : BaseState<HornetStateType>
{
	private HornetController hornet;
	private Vector2 backStepDirection;

	public HornetBackStepState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		backStepDirection = hornet.MoveDirection.normalized;
		hornet.StartCoroutine(BackStepCoroutine());
	}

	public override void FixedUpdate()
	{
		MoveBackStep();
	}

	private void MoveBackStep()
	{
		hornet.Rigid.velocity = -backStepDirection * hornet.BackStepPower;
	}

	IEnumerator BackStepCoroutine()
	{
		hornet.Animator.SetTrigger("BackStep");
		yield return new WaitForSeconds(hornet.BackStepTime);
		ChangeState(HornetStateType.Idle);
	}
}

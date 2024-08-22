using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;
using static VengeflyState;

public class HornetMoveState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetMoveState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.Animator.SetTrigger("Move");
		hornet.StartCoroutine(MoveCoroutine());
	}

	public override void Update()
	{
		if (hornet.IsMove)
		{
			MoveForward();
		}
		else
		{
			ChangeState(HornetStateType.Idle);
		}
	}

	private void MoveForward()
	{
		hornet.Rigid.velocity = hornet.MoveDirection * hornet.MoveSpeed;
	}

	IEnumerator MoveCoroutine()
	{
		hornet.IsMove = true;
		yield return new WaitForSeconds(hornet.MoveTime);
		hornet.IsMove = false;
	}
}

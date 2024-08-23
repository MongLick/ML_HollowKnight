using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HornetState;

public class HornetCircularAttackState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetCircularAttackState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(CircularAttackCoroutine());
	}

	public override void FixedUpdate()
	{
		CircularAttack();
	}

	private void CircularAttack()
	{

	}

	IEnumerator CircularAttackCoroutine()
	{
		hornet.Animator.SetTrigger("CircularAttack");
		hornet.Rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds(hornet.CircularAttackTime);
		hornet.Rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
		hornet.Rigid.velocity = new Vector2(hornet.Rigid.velocity.x, -1f);
		ChangeState(HornetStateType.Idle);
	}
}

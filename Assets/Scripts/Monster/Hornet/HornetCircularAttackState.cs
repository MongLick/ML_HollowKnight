using System.Collections;
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

	private IEnumerator CircularAttackCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetCircularAttack);
		hornet.OnCircularEvent?.Invoke();
		hornet.Animator.SetTrigger("CircularAttack");
		hornet.Rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		yield return new WaitForSeconds(hornet.CircularAttackTime);
		hornet.Rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
		hornet.Rigid.velocity = new Vector2(hornet.Rigid.velocity.x, -1f);
		ChangeState(HornetStateType.Idle);
	}
}

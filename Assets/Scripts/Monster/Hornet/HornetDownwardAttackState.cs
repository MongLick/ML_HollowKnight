using System.Collections;
using UnityEngine;
using static HornetState;

public class HornetDownwardAttackState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetDownwardAttackState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.PlayerPos = Manager.Game.Player.transform.position;
		hornet.TargetPos = new Vector2(hornet.PlayerPos.x, -12);
		hornet.StartCoroutine(DownwardAttackCoroutine());
	}

	private IEnumerator DownwardAttackCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetCircularAttack);
		hornet.Animator.SetTrigger("DownwardAttack");

		while (Vector2.Distance(hornet.transform.position, hornet.TargetPos) > 0.1f)
		{
			hornet.Direction = (hornet.TargetPos - (Vector2)hornet.transform.position).normalized;
			hornet.Rigid.velocity = hornet.Direction * hornet.AttackSpeed;

			if (hornet.IsGround)
			{
				break;
			}

			yield return null;
		}

		ChangeState(HornetStateType.Idle);
	}
}

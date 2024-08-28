using System.Collections;
using UnityEngine;
using static PlayerState;

public class PlayerDashState : BaseState<PlayerStateType>
{
	private PlayerController player;

	public PlayerDashState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		if (player.DashRoutine != null)
		{
			player.StopCoroutine(player.DashRoutine);
		}
		player.DashRoutine = player.StartCoroutine(DashCoroutine());

		player.OnDashEvent?.Invoke();
	}

	public override void Update()
	{
		if (player.IsTakeHit)
		{
			ChangeState(PlayerStateType.TakeHit);
		}
		else if (!player.IsDash)
		{
			ChangeState(PlayerStateType.Idle);
		}
	}

	private IEnumerator DashCoroutine()
	{
		player.CannotDash = true;
		player.IsDash = true;

		player.Animator.SetTrigger("Dash");

		player.Rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

		player.Rigid.velocity = player.LastMoveDir * player.DashSpeed;

		yield return new WaitForSeconds(player.DashTime);

		player.Rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

		if(!player.IsGround)
		{
			player.Rigid.velocity = new Vector2(0, -1f);
		}
		
		player.Renderer.flipX = player.LastMoveDir.x > 0;

		player.IsDash = false;
	}
}

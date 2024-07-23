using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static PlayerState;

/*public class PlayerJumpState : BaseState<PlayerStateType>
{
	private PlayerController player;
	float cu = 0;
	float max = 1.0f;
	bool t = false;

	public PlayerJumpState(PlayerController player)
	{
		this.player = player;
	}

	public override void Enter()
	{
		if (player.JumpChargingRoutine != null)
		{
			player.StopCoroutine(player.JumpChargingRoutine);
		}
		player.JumpChargingRoutine = player.StartCoroutine(JumpChargingRoutine());
	}

	public override void Update()
	{
		if (player.MoveDir != Vector2.zero && player.IsGround)
		{
			ChangeState(PlayerStateType.Move);
			return;
		}

		if (player.IsGround)
		{
			ChangeState(PlayerStateType.Idle);
			return;
		}
	}

	public override void FixedUpdate()
	{
		player.Animator.SetFloat("YSpeed", player.Rigid.velocity.y);

		if (player.IsJumpCharging)
		{
			Vector2 velocity = player.Rigid.velocity;
			velocity.y = player.CurrentJumpPower;
			player.Rigid.velocity = velocity;
		}
		if(t)
		{
			player.IsJumpCharging = false;
		}
	}

	public override void Exit()
	{
		t = false;
		cu = 0;
	}

	IEnumerator JumpChargingRoutine()
	{
		while(player.IsJumpCharging && !t)
		{
			cu += Time.deltaTime;
			if(cu >= max)
			{
				t = true;
			}
			yield return null;
		}
	}
}*/
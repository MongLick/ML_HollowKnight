using System.Collections;
using UnityEngine;
using static HornetState;

public class HornetIdleState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetIdleState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Update()
	{
		if(hornet.IsDie && Manager.Game.Player.IsDie)
		{
			return;
		}

		Vector2 playerPosition = Manager.Game.Player.transform.position;
		Vector2 currentPosition = hornet.transform.position;
		Vector2 playerDirection;

		playerDirection = new Vector2(playerPosition.x - currentPosition.x, 0).normalized;
		hornet.MoveDirection = playerDirection;
		if (playerDirection.x > 0)
		{
			hornet.Render.flipX = true;
		}
		else
		{
			hornet.Render.flipX = false;
		}
	}

	public override void Enter()
	{
		if (Manager.Game.Player.IsDie)
		{
			return;
		}
		hornet.StopAllCoroutines();
		hornet.Rigid.velocity = Vector2.zero;
		hornet.StartCoroutine(IdleCoroutine());
	}

	IEnumerator IdleCoroutine()
	{	
		int randomIndex = Random.Range(1, 6);
		HornetStateType randomState = (HornetStateType)randomIndex;
		yield return new WaitForSeconds(hornet.IdleTime);
		ChangeState(randomState);
	}
}

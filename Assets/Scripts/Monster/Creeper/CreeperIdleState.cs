using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CreeperIdleState : BaseState<CreeperStateType>
{
	private CreeperController creeper;

	public CreeperIdleState(CreeperController creeper)
	{
		this.creeper = creeper;
	}

	public override void Update()
	{
		CheckForPlayer();

		if (creeper.IsPlayerInRange)
		{
			ChangeState(CreeperStateType.Move);
		}
	}

	private void CheckForPlayer()
	{
		Collider2D[] players = Physics2D.OverlapCircleAll(creeper.transform.position, creeper.DetectionRadius, creeper.PlayerCheck);

		if (players != null)
		{
			creeper.IsPlayerInRange = players.Length > 0;
		}
	}
}

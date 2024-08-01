using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;

public class CreeperMoveState : BaseState<CreeperStateType>
{
	private CreeperController creeper;

	public CreeperMoveState(CreeperController creeper)
	{
		this.creeper = creeper;
	}

	public override void Enter()
	{
		Debug.Log("무브상태");
	}

	public override void Update()
	{
		
	}

	public override void Exit()
	{
		
	}
}

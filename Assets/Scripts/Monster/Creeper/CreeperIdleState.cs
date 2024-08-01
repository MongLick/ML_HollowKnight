using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreeperState;

public class CreeperIdleState : BaseState<CreeperStateType>
{
	private CreeperController creeper;

	public CreeperIdleState(CreeperController creeper)
	{
		this.creeper = creeper;
	}

	public override void Enter()
	{
		Debug.Log("���̵� ����");
	}

	public override void Update()
	{
		
	}

	public override void Exit()
	{
		Debug.Log("���̵� ����");
	}
}

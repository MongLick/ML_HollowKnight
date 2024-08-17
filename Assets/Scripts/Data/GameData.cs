using System;
using UnityEngine;

[Serializable]
public class GameData
{
	public float playerPosX;
	public float playerPosY;
	public float playerPosZ;

	public Vector3 PlayerPosition
	{
		get => new Vector3(playerPosX, playerPosY, playerPosZ);
		set
		{
			playerPosX = value.x;
			playerPosY = value.y;
			playerPosZ = value.z;
		}
	}
}

using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameData
{
	private int health = 5;
	public int Health { get { return health; } set { health = value; OnhealthChanged?.Invoke(value); } }
	private int coin = 0;
	public int Coin { get { return coin; } set { coin = value; OnCoinChanged?.Invoke(value); } }

	public UnityAction<int> OnhealthChanged;
	public UnityAction<int> OnCoinChanged;
}

using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameData
{
	[Header("UnityEvent")]
	[SerializeField] UnityAction<int> onhealthChanged;
	public UnityAction<int> OnhealthChanged { get { return onhealthChanged; } set { onhealthChanged = value; } }
	[SerializeField] UnityAction<int> onCoinChanged;
	public UnityAction<int> OnCoinChanged { get { return onCoinChanged; } set { onCoinChanged = value; } }

	[Header("Specs")]
	[SerializeField] int health = 5;
	public int Health { get { return health; } set { health = value; onhealthChanged?.Invoke(value); } }
	[SerializeField] int coin = 0;
	public int Coin { get { return coin; } set { coin = value; onCoinChanged?.Invoke(value); } }
}

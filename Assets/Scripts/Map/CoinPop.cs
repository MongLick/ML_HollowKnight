using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPop : MonoBehaviour, IDamageable
{
	[SerializeField] int hp;
	[SerializeField] Animator animator;
	[SerializeField] UnityEvent onHitCoinEvent;
	public UnityEvent OnHitCoinEvent { get { return onHitCoinEvent; } }

	public void TakeDamage(int damage)
	{
		animator.SetTrigger("CoinPop");
		hp -= damage;
		onHitCoinEvent.Invoke();
		if(hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}

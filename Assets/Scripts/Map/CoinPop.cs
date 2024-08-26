using UnityEngine;
using UnityEngine.Events;

public class CoinPop : MonoBehaviour, IDamageable
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onHitCoinEvent;
	public UnityEvent OnHitCoinEvent { get { return onHitCoinEvent; } set { onHitCoinEvent = value; } }

	[Header("Components")]
	[SerializeField] Animator animator;

	[Header("Specs")]
	[SerializeField] int hp;

	public void TakeDamage(int damage)
	{
		Manager.Sound.PlaySFX(Manager.Sound.CoinPop);
		animator.SetTrigger("CoinPop");
		hp -= damage;
		onHitCoinEvent.Invoke();
		if (hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}

using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IDamageable
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent<Monster> onHitCoinEvent;
	public UnityEvent<Monster> OnHitCoinEvent { get { return onHitCoinEvent; } set { onHitCoinEvent = value; } }
	[SerializeField] UnityEvent<Monster> onHitBloodEvent;
	public UnityEvent<Monster> OnHitBloodEvent { get { return onHitBloodEvent; } set { onHitBloodEvent = value; } }

	[Header("Components")]
	[SerializeField] Rigidbody2D rigid;
	public Rigidbody2D Rigid { get { return rigid; } set { rigid = value; } }

	[Header("Specs")]
	[SerializeField] int hp;
	public int Hp { get { return hp; } set { hp = value; } }
	[SerializeField] float knockbackPower;
	public float KnockbackPower { get { return knockbackPower; } }

	public virtual void ApplyKnockback(Vector2 direction)
	{
		rigid.AddForce(direction * knockbackPower, ForceMode2D.Impulse);
	}

	public virtual void TakeDamage(int damage, Transform hitPosition)
	{
		Manager.Sound.PlaySFX(Manager.Sound.MonsterTakeHit);
		hp -= damage;
		onHitBloodEvent?.Invoke(this);
		if (hp <= 0)
		{
			onHitCoinEvent?.Invoke(this);
		}
	}
}

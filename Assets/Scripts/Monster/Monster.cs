using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IDamageable
{
	[SerializeField] UnityEvent<Monster> onHitCoinEvent;
	public UnityEvent<Monster> OnHitCoinEvent { get { return onHitCoinEvent; } }
	[SerializeField] UnityEvent<Monster> onHitBloodEvent;
	public UnityEvent<Monster> OnHitBloodEvent { get { return onHitBloodEvent; } }
	[SerializeField] int hp;
	public int Hp { get { return hp; } set { hp = value; } }
	[SerializeField] Rigidbody2D rigid;
	public Rigidbody2D Rigid { get { return rigid; } set { rigid = value; } }
	[SerializeField] float knockbackPower;
	public float KnockbackPower { get { return knockbackPower; } }

	public void ApplyKnockback(Vector2 direction)
	{
		rigid.AddForce(direction * knockbackPower, ForceMode2D.Impulse);
	}

	public virtual void TakeDamage(int damage)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField] int hp;
	[SerializeField] int damage = 1;
	[SerializeField] LayerMask playerCheck;

	public void TakeDamage(int damage)
	{
		hp -= damage;
		Debug.Log(hp);
		if(hp <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(playerCheck.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();

			if (damageable != null)
			{
				damageable.TakeDamage(damage);
			}
		}
	}
}

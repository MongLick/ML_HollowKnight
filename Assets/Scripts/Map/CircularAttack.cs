using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularAttack : MonoBehaviour
{
	[SerializeField] private LayerMask playerCheck;
	[SerializeField] int damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerCheck.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage);
			}
		}
	}
}

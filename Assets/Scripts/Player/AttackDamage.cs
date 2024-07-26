using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
	[SerializeField] PlayerController player;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IDamageable damageable = collision.GetComponent<IDamageable>();

		if (damageable != null)
		{
			damageable.TakeDamage(player.Damage);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	[SerializeField] LayerMask playerCheckLayer;
	[SerializeField] int damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerCheckLayer.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage);
			}

			Manager.Game.RespawnPlayer();
		}
	}
}

using UnityEngine;

public class Spikes : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] LayerMask playerCheckLayer;

	[Header("Specs")]
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

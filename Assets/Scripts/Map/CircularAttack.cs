using UnityEngine;

public class CircularAttack : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] LayerMask playerCheck;

	[Header("Specs")]
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

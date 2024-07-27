using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackDamage : MonoBehaviour
{
	[SerializeField] PlayerController player;
	[SerializeField] UnityEvent<Vector3> onHitEvent = new UnityEvent<Vector3>();
	public UnityEvent<Vector3> OnHitEvent { get { return onHitEvent; } set { onHitEvent = value; } }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IDamageable damageable = collision.GetComponent<IDamageable>();

		if (damageable != null)
		{
			damageable.TakeDamage(player.Damage);
			onHitEvent?.Invoke(collision.transform.position);
		}
	}
}

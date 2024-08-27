using UnityEngine;
using UnityEngine.Events;

public class AttackDamage : MonoBehaviour
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent<Vector3> onHitEvent = new UnityEvent<Vector3>();
	public UnityEvent<Vector3> OnHitEvent { get { return onHitEvent; } set { onHitEvent = value; } }

	[Header("Components")]
	[SerializeField] PlayerController player;
	[SerializeField] LayerMask monsterCheck;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		IDamageable damageable = collision.GetComponent<IDamageable>();

		if (damageable != null)
		{
			damageable.TakeDamage(player.Damage, player.transform);
			onHitEvent?.Invoke(collision.transform.position);
		}
		if (monsterCheck.Contain(collision.gameObject.layer))
		{
			player.IsMonsterAttack = true;
			Monster monster = collision.GetComponent<Monster>();
			if (monster != null)
			{
				Vector2 attackDirection = player.LastAttackDirection;
				monster.ApplyKnockback(attackDirection);
			}
		}
	}
}

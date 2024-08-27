using UnityEngine;

public class Spear : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] SpriteRenderer render;
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] Collider2D hornetCollider;
	[SerializeField] LayerMask playerCheck;

	[Header("Vector2")]
	private Vector2 startPosition;
	private Vector2 direction;

	[Header("Specs")]
	[SerializeField] int damage;
	[SerializeField] float speed;
	[SerializeField] float maxDistance;
	private bool returning;

	private void OnEnable()
	{
		UpdateDirection();
	}

	private void FixedUpdate()
	{
		if (!returning)
		{
			rigid.velocity = direction * speed;

			if (Vector2.Distance(startPosition, rigid.position) >= maxDistance)
			{
				returning = true;
				direction = (startPosition - rigid.position).normalized;
				direction.y = 0;
			}
		}
		else
		{
			rigid.velocity = direction * speed;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerCheck.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage, transform);
			}
		}
	}

	private void UpdateDirection()
	{
		if (Manager.Game.Hornet != null)
		{
			returning = false;
			Collider2D hornetCollider = Manager.Game.Hornet.GetComponent<Collider2D>();
			startPosition = hornetCollider.bounds.center;
			transform.position = startPosition;

			Vector2 targetPosition = Manager.Game.Player.transform.position;
			direction = (targetPosition - startPosition).normalized;
			direction.y = 0;
			render.flipX = direction.x > 0;
		}
	}
}

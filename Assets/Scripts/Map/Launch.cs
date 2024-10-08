using UnityEngine;

public class Launch : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] SpriteRenderer render;

	[Header("Vector")]
	private Vector2 startPosition;
	private Vector2 direction;

	[Header("Specs")]
	[SerializeField] float offsetDistance;

	private void OnEnable()
	{
		if (Manager.Game.Hornet != null)
		{
			Collider2D hornetCollider = Manager.Game.Hornet.GetComponent<Collider2D>();
			Vector2 hornetPosition = hornetCollider.bounds.center;
			Vector2 playerPosition = Manager.Game.Player.transform.position;

			direction = (playerPosition - hornetPosition).normalized;
			render.flipX = direction.x > 0;

			if (direction.x > 0)
			{
				startPosition = hornetPosition - new Vector2(offsetDistance, 0);
			}
			else
			{
				startPosition = hornetPosition + new Vector2(offsetDistance, 0);
			}

			transform.position = startPosition;
		}
	}
}

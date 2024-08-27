using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDamageable
{
	[Header("Components")]
	[SerializeField] List<GameObject> stonePrefabs;
	[SerializeField] Rigidbody2D rigid;

	[Header("Vector")]
	private Vector2 randomPosition;
	private Vector2 forceDirection;
	private Vector2 force;

	[Header("Specs")]
	[SerializeField] int hp;
	[SerializeField] int stoneCount;
	[SerializeField] float rightForceMultiplier;
	[SerializeField] float angle;
	[SerializeField] float forceMagnitude;
	[SerializeField] float stoneSpawnRadius;
	[SerializeField] float minLaunchAngle;
	[SerializeField] float maxLaunchAngle;
	[SerializeField] float minForceMagnitude;
	[SerializeField] float maxForceMagnitude;


	public void TakeDamage(int damage, Transform hitPosition)
	{
		hp -= damage;
		Manager.Sound.PlaySFX(Manager.Sound.Door);
		if (hp <= 0)
		{
			for (int i = 0; i < stoneCount; i++)
			{
				randomPosition = (Vector2)transform.position + Random.insideUnitCircle * stoneSpawnRadius;

				GameObject selectedStonePrefab = stonePrefabs[Random.Range(0, stonePrefabs.Count)];

				GameObject stone = Instantiate(selectedStonePrefab, randomPosition, Quaternion.identity);

				angle = Random.Range(minLaunchAngle, maxLaunchAngle);
				forceDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;
				forceMagnitude = Random.Range(minForceMagnitude, maxForceMagnitude) * rightForceMultiplier;
				force = forceDirection * forceMagnitude;

				rigid = stone.GetComponent<Rigidbody2D>();
				if (rigid != null)
				{
					rigid.AddForce(force, ForceMode2D.Impulse);
				}
			}
			Destroy(gameObject);
		}
	}
}

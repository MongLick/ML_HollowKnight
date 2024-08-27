using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour, IDamageable
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onDirtmouthLoadScene;
	public UnityEvent OnDirtmouthUnloadScene { get { return onDirtmouthLoadScene; } set { onDirtmouthLoadScene = value; } }

	[Header("Components")]
	[SerializeField] List<GameObject> stonePrefabs;
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] SpriteRenderer render;
	[SerializeField] Sprite damagedSprite1;
	[SerializeField] Sprite damagedSprite2;

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

	private void UpdateSprite()
	{
		if (hp == 7)
		{
			render.sprite = damagedSprite1;
		}
		else if (hp == 4)
		{
			render.sprite = damagedSprite2;
		}
		else if (hp <= 0)
		{
			Manager.Sound.PlaySFX(Manager.Sound.Gate);
			gameObject.SetActive(false);
			onDirtmouthLoadScene?.Invoke();
		}
	}

	public void TakeDamage(int damage, Transform hitPosition)
	{
		Manager.Sound.PlaySFX(Manager.Sound.Door);
		hp -= damage;
		for (int i = 0; i < stoneCount; i++)
		{
			randomPosition = (Vector2)transform.position + Random.insideUnitCircle * stoneSpawnRadius;

			GameObject selectedStonePrefab = stonePrefabs[Random.Range(0, stonePrefabs.Count)];

			GameObject stone = Instantiate(selectedStonePrefab, randomPosition, Quaternion.identity);

			angle = Random.Range(minLaunchAngle, maxLaunchAngle);
			forceDirection = Quaternion.Euler(0, 0, angle) * Vector2.left;
			forceMagnitude = Random.Range(minForceMagnitude, maxForceMagnitude) * rightForceMultiplier;
			force = forceDirection * forceMagnitude;

			rigid = stone.GetComponent<Rigidbody2D>();
			if (rigid != null)
			{
				rigid.AddForce(force, ForceMode2D.Impulse);
			}
		}

		UpdateSprite();
	}
}

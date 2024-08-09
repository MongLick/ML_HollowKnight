using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] Collider2D playerCollider;
	public Collider2D PlayerCollider { get {  return playerCollider; } }
	[SerializeField] List<Collider2D> monsterColliders = new List<Collider2D>();
	public List<Collider2D> MonsterColliders { get { return monsterColliders; } }
	[SerializeField] LayerMask monsterLayer;
	[SerializeField] Transform respawnPoint;

	public void UpdateMonsterColliders()
	{
		playerCollider = null;
		monsterColliders.Clear();
		respawnPoint = null;

		playerCollider = FindAnyObjectByType<PlayerController>().GetComponent<Collider2D>();
		var allMonsters = FindObjectsOfType<Collider2D>();
		var spawnPointObject = FindAnyObjectByType<SpawnPoint>();
		respawnPoint = spawnPointObject?.transform;

		foreach (var collider in allMonsters)
		{
			if (collider != null && monsterLayer.Contain(collider.gameObject.layer))
			{
				monsterColliders.Add(collider);
			}
		}
	}

	public void RespawnPlayer(Transform playerTransform)
	{
		if (playerTransform != null)
		{
			playerTransform.position = respawnPoint.position;
		}
	}
}

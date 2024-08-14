using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] PlayerController player;
	[SerializeField] Collider2D playerCollider;
	public Collider2D PlayerCollider { get { return playerCollider; } }
	[SerializeField] List<Collider2D> monsterColliders = new List<Collider2D>();
	public List<Collider2D> MonsterColliders { get { return monsterColliders; } }
	[SerializeField] LayerMask monsterLayer;
	[SerializeField] Transform respawnPoint;
	public Transform RespawnPoint { get { return respawnPoint; } }
	[SerializeField] float respawnDelay; 

	public void GameManagerUpdate()
	{
		player = null;
		playerCollider = null;
		monsterColliders.Clear();
		respawnPoint = null;

		player = FindAnyObjectByType<PlayerController>();
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

	public void RespawnPlayer()
	{
		StartCoroutine(RespawnPlayerCoroutine());
	}

	private IEnumerator RespawnPlayerCoroutine()
	{
		Manager.Scene.LoadFadeOut();

		player.Rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		player.Rigid.velocity = Vector2.zero;

		yield return new WaitForSeconds(respawnDelay);
		player.RespawnPlayer();
		player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		Manager.Scene.LoadFadeIn();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] string previousSceneName;
	[SerializeField] PlayerController player;
	public PlayerController Player { get { return player; } }
	[SerializeField] HornetController hornet;
	public HornetController Hornet { get { return hornet; } }
	[SerializeField] Collider2D playerCollider;
	public Collider2D PlayerCollider { get { return playerCollider; } }
	[SerializeField] List<Collider2D> monsterColliders = new List<Collider2D>();
	public List<Collider2D> MonsterColliders { get { return monsterColliders; } }
	[SerializeField] LayerMask monsterLayer;
	[SerializeField] Transform respawnPoint;
	public Transform RespawnPoint { get { return respawnPoint; } }
	[SerializeField] float respawnDelay;
	[SerializeField] Gate gate;
	[SerializeField] GatePoint gatePoint;
	[SerializeField] Texture2D customCursorTexture;

	private void Start()
	{
		Cursor.SetCursor(customCursorTexture, Vector2.zero, CursorMode.Auto);
	}

	public void SetPreviousScene(string sceneName)
	{
		previousSceneName = sceneName;
	}

	public string GetPreviousScene()
	{
		return previousSceneName;
	}

	public Vector3 GetStartPosition(string sceneName, string previousSceneName)
	{
		if (sceneName == "KingsPassScene")
		{
			if (previousSceneName == "KingsPassScene")
			{
				return new Vector3(8.5f, -1f, 0);
			}
		}

		if (sceneName == "KingsPassScene")
		{
			if (previousSceneName == "TitleScene")
			{
				return new Vector3(8.5f, 19.5f, 0);
			}
			else if (previousSceneName == "DirtmouthScene")
			{
				gate.gameObject.SetActive(false);
				gatePoint.gameObject.GetComponent<Collider2D>().enabled = true;
				return new Vector3(136.5f, 45f, 0);
			}
		}

		if (sceneName == "DirtmouthScene")
		{
			if (previousSceneName == "KingsPassScene")
			{
				return new Vector3(-24, -4f, 0);
			}
			else if (previousSceneName == "CrossroadsScene")
			{
				return new Vector3(23.5f, -17f, 0);
			}
		}

		if (sceneName == "CrossroadsScene")
		{
			if (previousSceneName == "DirtmouthScene")
			{
				return new Vector3(-28.3f, -9f, 0);
			}
		}

		if (sceneName == "BossScene")
		{
			if (previousSceneName == "CrossroadsScene")
			{
				return new Vector3(1.8f, -9.5f, 0);
			}
		}

		if (sceneName == "TitleScene")
		{
			if (previousSceneName == "BossScene")
			{
				Manager.UI.OnTitleSceneLoad();
			}
		}

		return Vector3.zero;
	}

	public void OnSceneTransition(string newSceneName)
	{
		string previousSceneName = GetPreviousScene();
		Vector3 startPosition = GetStartPosition(newSceneName, previousSceneName);

		if (player != null)
		{
			player.transform.position = startPosition;
			player.RestoreChildPositions();
		}
	}

	public void KingsPassUpdate()
	{
		player = null;
		playerCollider = null;
		monsterColliders.Clear();
		respawnPoint = null;
		gate = null;

		player = FindAnyObjectByType<PlayerController>();
		playerCollider = FindAnyObjectByType<PlayerController>().GetComponent<Collider2D>();
		var allMonsters = FindObjectsOfType<Collider2D>();
		var spawnPointObject = FindAnyObjectByType<SpawnPoint>();
		respawnPoint = spawnPointObject?.transform;
		gate = FindAnyObjectByType<Gate>();
		gatePoint = FindAnyObjectByType<GatePoint>();

		foreach (var collider in allMonsters)
		{
			if (collider != null && monsterLayer.Contain(collider.gameObject.layer))
			{
				monsterColliders.Add(collider);
			}
		}
	}

	public void DirtmouthUpdate()
	{
		player = null;
		playerCollider = null;

		player = FindAnyObjectByType<PlayerController>();
		playerCollider = FindAnyObjectByType<PlayerController>().GetComponent<Collider2D>();
	}

	public void CrossroadsUpdate()
	{
		player = null;
		playerCollider = null;
		monsterColliders.Clear();

		player = FindAnyObjectByType<PlayerController>();
		playerCollider = FindAnyObjectByType<PlayerController>().GetComponent<Collider2D>();
		var allMonsters = FindObjectsOfType<Collider2D>();

		foreach (var collider in allMonsters)
		{
			if (collider != null && monsterLayer.Contain(collider.gameObject.layer))
			{
				monsterColliders.Add(collider);
			}
		}
	}

	public void BossUpdate()
	{
		player = null;
		playerCollider = null;
		monsterColliders.Clear();
		hornet = null;

		player = FindAnyObjectByType<PlayerController>();
		playerCollider = FindAnyObjectByType<PlayerController>().GetComponent<Collider2D>();
		hornet = FindAnyObjectByType<HornetController>();
		var allMonsters = FindObjectsOfType<Collider2D>();

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

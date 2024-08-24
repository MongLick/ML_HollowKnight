using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
	[SerializeField] PlayerController player;
	[SerializeField] HornetController hornet;
	[SerializeField] List<Monster> monsters;
	[SerializeField] CoinPop coinPop;
	[SerializeField] HealingChair healingChair;
	[SerializeField] List<AttackDamage> hits;
	[SerializeField] Transform dustPos;
	[SerializeField] Transform rightDashPos;
	[SerializeField] Transform leftDashPos;
	[SerializeField] PooledObject dustPrefab;
	[SerializeField] PooledObject hitPrefab;
	[SerializeField] PooledObject dashPrefab;
	[SerializeField] PooledObject playerTakeHitPrefab;
	[SerializeField] PooledObject coinPrefab;
	[SerializeField] PooledObject bloodPrefab;
	[SerializeField] PooledObject healPrefab;
	[SerializeField] PooledObject spearPrefab;
	[SerializeField] PooledObject launchPrefab;
	[SerializeField] PooledObject circularPrefab;

	private void Start()
	{
		Manager.Scene.OnLoadScene.AddListener(OnSceneLoaded);

		InitializePools();
	}

	private void OnSceneLoaded(string name)
	{
		switch (name)
		{
			case "TitleScene":
				HandleTitleScene();
				break;
			case "KingsPassScene":
			case "DirtmouthScene":
			case "CrossroadsScene":
			case "BossScene":
				HandleGameScene();
				break;
		}
	}

	public void InitializePools()
	{
		Manager.Pool.CreatePool(dustPrefab, 4, 10);
		Manager.Pool.CreatePool(hitPrefab, 4, 10);
		Manager.Pool.CreatePool(dashPrefab, 1, 3);
		Manager.Pool.CreatePool(playerTakeHitPrefab, 1, 3);
		Manager.Pool.CreatePool(coinPrefab, 5, 10);
		Manager.Pool.CreatePool(bloodPrefab, 1, 2);
		Manager.Pool.CreatePool(healPrefab, 1, 2);
		Manager.Pool.CreatePool(spearPrefab, 1, 2);
		Manager.Pool.CreatePool(launchPrefab, 1, 2);
		Manager.Pool.CreatePool(circularPrefab, 1, 2);
	}

	private void HandleTitleScene()
	{
		player = null;
		monsters = new List<Monster>();
		coinPop = null;
		hits = new List<AttackDamage>();
		dustPos = null;
		rightDashPos = null;
		leftDashPos = null;
		healingChair = null;
		hornet = null;
	}

	private void HandleGameScene()
	{
		player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
		hornet = GameObject.FindGameObjectWithTag("Hornet")?.GetComponent<HornetController>();
		coinPop = GameObject.FindGameObjectWithTag("CoinPop")?.GetComponent<CoinPop>();
		healingChair = GameObject.FindGameObjectWithTag("HealingChair")?.GetComponent<HealingChair>();

		GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("Monster");
		monsters = new List<Monster>();
		foreach (GameObject monsterObject in monsterObjects)
		{
			Monster monster = monsterObject.GetComponent<Monster>();
			if (monster != null)
			{
				monsters.Add(monster);
			}
		}

		hits = new List<AttackDamage>(FindObjectsOfType<AttackDamage>(true));

		dustPos = GameObject.FindGameObjectWithTag("GroundCheck")?.transform;
		rightDashPos = GameObject.FindGameObjectWithTag("RightDash")?.transform;
		leftDashPos = GameObject.FindGameObjectWithTag("LeftDash")?.transform;

		SetupEventListeners();
	}

	private void SetupEventListeners()
	{
		if (player != null)
		{
			player.OnJumpEvent.AddListener(SpawnDustOnJump);
			player.OnFallEvent.AddListener(SpawnDustOnFall);
			player.OnDashEvent.AddListener(SpawnDash);
			player.OnTakeHitEvent.AddListener(SpawnTakeHit);

			foreach (var hit in hits)
			{
				hit.OnHitEvent.AddListener(SpawnHit);
			}
			if (coinPop != null)
			{
				coinPop.OnHitCoinEvent.AddListener(SpawnPopCoin);
			}
			if (monsters != null)
			{
				foreach (var monster in monsters)
				{
					monster.OnHitCoinEvent.AddListener(SpawnMonserCoin);
					monster.OnHitBloodEvent.AddListener(SpawnBlood);
				}
			}
			if (healingChair != null)
			{
				healingChair.OnHealingEvent.AddListener(SpawnHeal);
			}
			if(hornet != null)
			{
				hornet.OnSpearThrowEvent.AddListener(SpawnSpear);
				hornet.OnLaunchEvent.AddListener(SpawnLaunch);
				hornet.OnCircularEvent.AddListener(SpawnCircular);
			}
		}
	}

	private void SpawnDustOnJump()
	{
		if (dustPrefab != null && dustPos != null)
		{
			PooledObject instance = Manager.Pool.GetPool(dustPrefab, dustPos.position, Quaternion.identity);

			EffectAnimation dustAnimation = instance.GetComponent<EffectAnimation>();
			if (dustAnimation != null)
			{
				dustAnimation.PlayDustJumpAnimation();
			}
		}
	}

	private void SpawnDustOnFall()
	{
		if (dustPrefab != null && dustPos != null)
		{
			PooledObject instance = Manager.Pool.GetPool(dustPrefab, dustPos.position, Quaternion.identity);

			EffectAnimation dustAnimation = instance.GetComponent<EffectAnimation>();
			if (dustAnimation != null)
			{
				dustAnimation.PlayDustFallAnimation();
			}
		}
	}

	private void SpawnHit(Vector3 hitPosition)
	{
		if (hitPrefab != null)
		{
			PooledObject instance = Manager.Pool.GetPool(hitPrefab, hitPosition, Quaternion.identity);

			instance.transform.position = hitPosition;

			EffectAnimation hitAnimation = instance.GetComponent<EffectAnimation>();
			if (hitAnimation != null)
			{
				hitAnimation.PlayHitAnimation();
			}
		}
	}

	private void SpawnDash()
	{
		if (dashPrefab != null)
		{
			if (player.LastMoveDir.x > 0)
			{
				PooledObject instance = Manager.Pool.GetPool(dashPrefab, leftDashPos.position, Quaternion.identity);
				EffectAnimation dashAnimation = instance.GetComponent<EffectAnimation>();

				if (dashAnimation != null)
				{
					dashAnimation.PlayDashAnimation();
				}
			}
			else
			{
				PooledObject instance = Manager.Pool.GetPool(dashPrefab, rightDashPos.position, Quaternion.identity);
				EffectAnimation dashAnimation = instance.GetComponent<EffectAnimation>();

				if (dashAnimation != null)
				{
					dashAnimation.PlayDashAnimation();
				}
			}
		}
	}

	private void SpawnTakeHit()
	{
		if (playerTakeHitPrefab != null)
		{
			PooledObject instance = Manager.Pool.GetPool(playerTakeHitPrefab, player.transform.position, Quaternion.identity);
			EffectAnimation takeHitAnimation = instance.GetComponent<EffectAnimation>();

			if (takeHitAnimation != null)
			{
				takeHitAnimation.PlayTakeHitAnimation();
			}
		}
	}

	private void SpawnMonserCoin(Monster monster)
	{
		if (coinPrefab != null)
		{
			int coinCount = Random.Range(1, 4);
			for (int i = 0; i < coinCount; i++)
			{
				Vector2 monsterPosition = monster.transform.position;
				Vector2 randomOffset = new Vector2(Random.Range(0, 1f), Random.Range(0f, 1f));

				Vector2 spawnPosition = monsterPosition + randomOffset;

				PooledObject instance = Manager.Pool.GetPool(coinPrefab, spawnPosition, Quaternion.identity);
				Coin coin = instance.GetComponent<Coin>();
				if (coin != null)
				{
					Vector3 bounceDirection = new Vector3(0, 10, 0);
					coin.Initialize(bounceDirection);
				}
			}
		}
	}

	private void SpawnPopCoin()
	{
		int coinCount = Random.Range(3, 6);

		for (int i = 0; i < coinCount; i++)
		{
			Vector2 coinPopPosition = coinPop.transform.position;
			Vector2 randomOffset = new Vector2(Random.Range(0, 1f), Random.Range(0, 1f));

			Vector2 spawnPosition = coinPopPosition + randomOffset;

			PooledObject instance = Manager.Pool.GetPool(coinPrefab, spawnPosition, Quaternion.identity);
			Coin coin = instance.GetComponent<Coin>();
			if (coin != null)
			{
				Vector3 bounceDirection = new Vector3(0, 10, 0);
				coin.Initialize(bounceDirection);
			}
		}
	}

	private void SpawnBlood(Monster monster)
	{
		Vector2 bloodPosition = monster.transform.position;
		PooledObject instance = Manager.Pool.GetPool(bloodPrefab, bloodPosition, Quaternion.identity);

		EffectAnimation bloodAnimation = instance.GetComponent<EffectAnimation>();
		if (bloodAnimation != null)
		{
			bloodAnimation.PlayHitBloodAnimation();
		}
	}

	private void SpawnHeal()
	{
		Vector2 playerPosition = player.transform.position;
		PooledObject instance = Manager.Pool.GetPool(healPrefab, playerPosition, Quaternion.identity);

		EffectAnimation healAnimation = instance.GetComponent<EffectAnimation>();
		if (healAnimation != null)
		{
			healAnimation.PlayHealAnimation();
		}
	}

	private void SpawnSpear()
	{
		Vector2 hornetPosition = hornet.transform.position;
		PooledObject instance = Manager.Pool.GetPool(spearPrefab, hornetPosition, Quaternion.identity);

		EffectAnimation spearAnimation = instance.GetComponent<EffectAnimation>();
		if (spearAnimation != null)
		{
			spearAnimation.PlaySpear();
		}
	}

	private void SpawnLaunch()
	{
		Vector2 hornetPosition = hornet.transform.position;
		PooledObject instance = Manager.Pool.GetPool(launchPrefab, hornetPosition, Quaternion.identity);

		EffectAnimation launchAnimation = instance.GetComponent<EffectAnimation>();
		if (launchAnimation != null)
		{
			launchAnimation.PlayLaunch();
		}
	}

	private void SpawnCircular()
	{
		Collider2D hornetCollider = Manager.Game.Hornet.GetComponent<Collider2D>();
		Vector2 hornetPosition = hornetCollider.bounds.center;

		PooledObject instance = Manager.Pool.GetPool(circularPrefab, hornetPosition, Quaternion.identity);

		EffectAnimation circularAnimation = instance.GetComponent<EffectAnimation>();
		if (circularAnimation != null)
		{
			circularAnimation.PlayCircular();
		}
	}
}

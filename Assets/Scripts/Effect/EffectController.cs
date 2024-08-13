using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour
{
	[SerializeField] PlayerController player;
	[SerializeField] List<Monster> monsters;
	[SerializeField] CoinPop coinPop;
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

	private void Awake()
	{
		Manager.Pool.CreatePool(dustPrefab, 4, 10);
		Manager.Pool.CreatePool(hitPrefab, 4, 10);
		Manager.Pool.CreatePool(dashPrefab, 1, 3);
		Manager.Pool.CreatePool(playerTakeHitPrefab, 1, 3);
		Manager.Pool.CreatePool(coinPrefab, 5, 10);
		Manager.Pool.CreatePool(bloodPrefab, 1, 2);
	}

	private void Start()
	{
		if (player != null)
		{
			player.OnJumpEvent.AddListener(SpawnDustOnJump);
			player.OnFallEvent.AddListener(SpawnDustOnFall);
			player.OnDashEvent.AddListener(SpawnDash);
			player.OnTakeHitEvent.AddListener(SpawnTakeHit);
			coinPop.OnHitCoinEvent.AddListener(SpawnPopCoin);
			foreach (var hit in hits)
			{
				hit.OnHitEvent.AddListener(SpawnHit);
			}
			foreach (var monster in monsters)
			{
				monster.OnHitCoinEvent.AddListener(SpawnMonserCoin);
				monster.OnHitBloodEvent.AddListener(SpawnBlood);
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
			if(player.LastMoveDir.x > 0)
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
				Vector2 randomOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

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
}

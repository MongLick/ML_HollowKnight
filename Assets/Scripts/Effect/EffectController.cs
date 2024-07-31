using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour
{
	[SerializeField] PlayerController player;
	[SerializeField] List<AttackDamage> hits;
	[SerializeField] Transform dustPos;
	[SerializeField] Transform rightDashPos;
	[SerializeField] Transform leftDashPos;
	[SerializeField] PooledObject dustPrefab;
	[SerializeField] PooledObject hitPrefab;
	[SerializeField] PooledObject dashPrefab;
	[SerializeField] PooledObject playerTakeHitPrefab;

	private void Awake()
	{
		Manager.Pool.CreatePool(dustPrefab, 4, 10);
		Manager.Pool.CreatePool(hitPrefab, 4, 10);
		Manager.Pool.CreatePool(dashPrefab, 1, 3);
		Manager.Pool.CreatePool(playerTakeHitPrefab, 1, 3);
	}

	private void Start()
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
}

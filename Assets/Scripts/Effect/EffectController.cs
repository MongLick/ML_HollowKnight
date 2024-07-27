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
	[SerializeField] PooledObject dustPrefab;
	[SerializeField] PooledObject hitPrefab;

	private void Awake()
	{
		Manager.Pool.CreatePool(dustPrefab, 4, 10);
		Manager.Pool.CreatePool(hitPrefab, 4, 10);
	}

	private void Start()
	{
		if (player != null)
		{
			player.OnJumpEvent.AddListener(SpawnDustOnJump);
			player.OnFallEvent.AddListener(SpawnDustOnFall);
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
}

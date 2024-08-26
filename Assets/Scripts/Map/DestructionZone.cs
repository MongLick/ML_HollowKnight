using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionZone : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] List<GameObject> groundObjects;
	[SerializeField] GameObject breakEffect;
	[SerializeField] Transform breakPos1;
	[SerializeField] Transform breakPos2;
	[SerializeField] Transform breakPos3;
	[SerializeField] LayerMask playerCheckLayer;

	[Header("Specs")]
	[SerializeField] float delay;
	private bool effectTriggered;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerCheckLayer.Contain(collision.gameObject.layer))
		{
			TriggerGround();
		}
	}

	private void TriggerGround()
	{
		effectTriggered = true;

		foreach (var ground in groundObjects)
		{
			Animator animator = ground.GetComponent<Animator>();
			if (animator != null)
			{
				animator.SetTrigger("Break");
			}
			StartCoroutine(DestroyAfterDelay(ground, delay));
		}
	}

	private void CreateEffectAtPosition(Transform position)
	{
		if (breakEffect != null)
		{
			Manager.Sound.PlaySFX(Manager.Sound.Gate);
			GameObject effectInstance = Instantiate(breakEffect, position.position, Quaternion.Euler(0, 0, -90));
			ParticleSystem ps = effectInstance.GetComponent<ParticleSystem>();
			if (ps != null)
			{
				ps.Play();
			}
			StartCoroutine(DestroyEffectAfterTime(effectInstance, 2f));
		}
	}

	private IEnumerator DestroyAfterDelay(GameObject ground, float delay)
	{
		yield return new WaitForSeconds(delay);
		ground.gameObject.SetActive(false);
		if (effectTriggered)
		{
			CreateEffectAtPosition(breakPos1);
			CreateEffectAtPosition(breakPos2);
			CreateEffectAtPosition(breakPos3);
			effectTriggered = false;
		}
	}

	private IEnumerator DestroyEffectAfterTime(GameObject effectInstance, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(effectInstance);
	}
}

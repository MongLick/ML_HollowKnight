using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionZone : MonoBehaviour
{
	[SerializeField] LayerMask playerCheckLayer;
	[SerializeField] List<GameObject> groundObjects;
	[SerializeField] float delay;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(playerCheckLayer.Contain(collision.gameObject.layer))
		{
			TriggerGround();
		}
	}

	private void TriggerGround()
	{
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

	private IEnumerator DestroyAfterDelay(GameObject ground, float delay)
	{
		yield return new WaitForSeconds(delay);
		ground.gameObject.SetActive(false);
	}
}

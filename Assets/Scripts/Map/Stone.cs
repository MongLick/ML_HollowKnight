using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	[SerializeField] float destroyTime;

	private void Start()
	{
		StartCoroutine(DestroyTimeCoroutine());
	}

	private IEnumerator DestroyTimeCoroutine()
	{
		yield return new WaitForSeconds(destroyTime);
		Destroy(gameObject);
	}
}

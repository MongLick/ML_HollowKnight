using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
	[Header("Specs")]
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

using System.Collections;
using UnityEngine;

public class BossTextZone : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] CanvasGroup bossCanvas;
	[SerializeField] BoxCollider2D boxCollider;
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	[SerializeField] float fadeDuration;
	[SerializeField] float fadeDely;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			boxCollider.enabled = false;
			StartCoroutine(FadeIn());
		}
	}

	private IEnumerator FadeIn()
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			bossCanvas.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
			yield return null;
		}
		bossCanvas.alpha = 1f;
		yield return new WaitForSeconds(fadeDely);
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut()
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			bossCanvas.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
			yield return null;
		}
		bossCanvas.alpha = 0f;
	}
}

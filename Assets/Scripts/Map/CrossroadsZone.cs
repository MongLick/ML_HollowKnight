using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrossroadsZone : MonoBehaviour
{
	[SerializeField] LayerMask playerLayer;
	[SerializeField] CanvasGroup crossroadsZoneCanvas;
	[SerializeField] float fadeDuration;
	[SerializeField] UnityEvent onCrossroadsScene;
	public UnityEvent OnCrossroadsScene { get { return onCrossroadsScene; } }
	[SerializeField] bool isPlayerTrigger;
	[SerializeField] bool isSceneChange;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			StartCoroutine(FadeIn(crossroadsZoneCanvas));
			isPlayerTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isSceneChange && gameObject.activeInHierarchy)
		{
			StartCoroutine(FadeOut(crossroadsZoneCanvas));
			isPlayerTrigger = false;
		}
	}

	private IEnumerator FadeIn(CanvasGroup canvasGroup)
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
			yield return null;
		}
		canvasGroup.alpha = 1f;
	}

	private IEnumerator FadeOut(CanvasGroup canvasGroup)
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
			yield return null;
		}
		canvasGroup.alpha = 0f;
	}

	private void Update()
	{
		if(isPlayerTrigger && Input.GetKeyDown(KeyCode.V))
		{
			isSceneChange = true;
			onCrossroadsScene?.Invoke();
			return;
		}
	}
}

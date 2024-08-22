using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpDestructionZone : MonoBehaviour
{
	[SerializeField] LayerMask playerLayer;
	[SerializeField] CanvasGroup UpDirtmouthZoneCanvas;
	[SerializeField] float fadeDuration;
	[SerializeField] UnityEvent onDirtmouthScene;
	public UnityEvent OnDirtmouthScene { get { return onDirtmouthScene; } }
	[SerializeField] bool isPlayerTrigger;
	[SerializeField] bool isSceneChange;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			StartCoroutine(FadeIn(UpDirtmouthZoneCanvas));
			isPlayerTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isSceneChange && gameObject.activeInHierarchy)
		{
			StartCoroutine(FadeOut(UpDirtmouthZoneCanvas));
			isPlayerTrigger = false;
		}
	}

	public void SceneChange()
	{
		if (isPlayerTrigger)
		{
			isSceneChange = true;
			onDirtmouthScene?.Invoke();
			return;
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
}

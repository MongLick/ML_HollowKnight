using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossZone : MonoBehaviour
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onBossScene;
	public UnityEvent OnBossScene { get { return onBossScene; } set { onBossScene = value; } }

	[Header("Components")]
	[SerializeField] CanvasGroup BossZoneCanvas;
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	[SerializeField] float fadeDuration;
	private bool isPlayerTrigger;
	private bool isSceneChange;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			StartCoroutine(FadeIn(BossZoneCanvas));
			isPlayerTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isSceneChange && gameObject.activeInHierarchy)
		{
			StartCoroutine(FadeOut(BossZoneCanvas));
			isPlayerTrigger = false;
		}
	}

	public void SceneChange()
	{
		if (isPlayerTrigger)
		{
			isSceneChange = true;
			OnBossScene?.Invoke();
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

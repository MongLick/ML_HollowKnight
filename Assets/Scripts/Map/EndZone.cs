using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EndZone : MonoBehaviour
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onTitleScene;
	public UnityEvent OnTitleScene { get { return onTitleScene; } set { onTitleScene = value; } }

	[Header("Components")]
	[SerializeField] HornetController hornet;
	[SerializeField] CanvasGroup EndCanvas;
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	[SerializeField] float fadeDuration;
	[SerializeField] bool isPlayerTrigger;
	[SerializeField] bool isSceneChange;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && hornet.IsDie)
		{
			StartCoroutine(FadeIn(EndCanvas));
			isPlayerTrigger = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isSceneChange && gameObject.activeInHierarchy && hornet.IsDie)
		{
			StartCoroutine(FadeOut(EndCanvas));
			isPlayerTrigger = false;
		}
	}

	public void SceneChange()
	{
		if (isPlayerTrigger && hornet.IsDie && !isSceneChange)
		{
			isSceneChange = true;
			onTitleScene?.Invoke();
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

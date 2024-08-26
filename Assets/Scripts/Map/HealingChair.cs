using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealingChair : MonoBehaviour
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onHealingEvent;
	public UnityEvent OnHealingEvent { get { return onHealingEvent; } set { onHealingEvent = value; } }

	[Header("Components")]
	[SerializeField] CanvasGroup saveCanvas;
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	[SerializeField] float fadeDuration;
	private bool isPlayerTrigger;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			if (!isPlayerTrigger)
			{
				isPlayerTrigger = true;
				StartCoroutine(FadeIn(saveCanvas));
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && gameObject.activeInHierarchy)
		{
			StartCoroutine(FadeOut(saveCanvas));
			isPlayerTrigger = false;
		}
	}

	public void HealPlayer()
	{
		if (Manager.Data.GameData.Health >= 5 && isPlayerTrigger)
		{
			return;
		}
		Manager.Sound.PlaySFX(Manager.Sound.PlayerHeal);
		onHealingEvent?.Invoke();
		Manager.Data.GameData.Health = 5;
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

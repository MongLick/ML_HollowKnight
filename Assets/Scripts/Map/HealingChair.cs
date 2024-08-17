using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealingChair: MonoBehaviour
{
	[SerializeField] CanvasGroup saveCanvas;
	[SerializeField] LayerMask playerLayer;
	[SerializeField] float fadeDuration;
	[SerializeField] bool isPlayerTrigger;
	[SerializeField] UnityEvent onHealingEvent;
	public UnityEvent OnHealingEvent { get { return onHealingEvent; } }

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
		if (Input.GetKeyDown(KeyCode.V) && isPlayerTrigger)
		{
			HealPlayer();
		}
	}

	private void HealPlayer()
	{
		onHealingEvent?.Invoke();
		Manager.Game.Player.Hp = 5;
	}
}
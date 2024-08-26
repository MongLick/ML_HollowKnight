using System.Collections;
using TMPro;
using UnityEngine;

public class ElderbugController : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] ElderbugPlayerCheck playerChack;
	[SerializeField] CanvasGroup dialogueCanvasGroup;
	[SerializeField] CanvasGroup dialogueCanvasGroup2;
	[SerializeField] TMP_Text dialogueText;
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	[SerializeField] string[] dialogueLines;
	[SerializeField] int currentLineIndex;
	[SerializeField] float fadeDuration;
	private bool isTyping;
	private bool isPlayerTrigger;
	private bool isTypingEnd;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			if (!isPlayerTrigger)
			{
				isPlayerTrigger = true;
				StartCoroutine(FadeIn(dialogueCanvasGroup));
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && gameObject.activeInHierarchy)
		{
			if (dialogueCanvasGroup.alpha != 0)
			{
				StartCoroutine(FadeOut(dialogueCanvasGroup));

			}
			if (currentLineIndex != 0)
			{
				StartCoroutine(FadeOut(dialogueCanvasGroup2));
				currentLineIndex = 0;
			}

			isPlayerTrigger = false;
		}
	}

	public void Talk()
	{
		if (!isTyping && isPlayerTrigger)
		{
			if (currentLineIndex == dialogueLines.Length)
			{
				StartCoroutine(FadeOut(dialogueCanvasGroup2));
				StartCoroutine(FadeIn(dialogueCanvasGroup));
				currentLineIndex = 0;
				return;
			}

			if (currentLineIndex == 0)
			{
				StartCoroutine(FadeOut(dialogueCanvasGroup));
				StartCoroutine(FadeIn(dialogueCanvasGroup2));
			}

			if (currentLineIndex < dialogueLines.Length)
			{
				Manager.Sound.PlaySFX(Manager.Sound.Elderbug[currentLineIndex]);
				StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
				currentLineIndex++;
			}
		}

		if (!isPlayerTrigger && currentLineIndex != 0)
		{
			StartCoroutine(FadeOut(dialogueCanvasGroup2));
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

	private IEnumerator TypeSentence(string sentence)
	{
		if (playerChack.IsPlayerLeft)
		{
			animator.SetBool("LeftTalk", true);
		}
		else
		{
			animator.SetBool("RightTalk", true);
		}
		isTyping = true;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
		isTyping = false;
		animator.SetBool("LeftTalk", false);
		animator.SetBool("RightTalk", false);
	}
}

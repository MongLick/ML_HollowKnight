using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.UI;

public class ElderbugController : MonoBehaviour
{
	[SerializeField] ElderbugPlayerCheck playerChack;
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] LayerMask playerLayer;
	[SerializeField] float fadeDuration;
	[SerializeField] CanvasGroup dialogueCanvasGroup;
	[SerializeField] CanvasGroup dialogueCanvasGroup2;
	[SerializeField] TMP_Text dialogueText;

	[SerializeField] string[] dialogueLines;
	[SerializeField] bool isTyping;
	[SerializeField] bool isPlayerTrigger;
	[SerializeField] bool isTypingEnd;

	[SerializeField] int currentLineIndex = 0;


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
			if(currentLineIndex != 0)
			{
				StartCoroutine(FadeOut(dialogueCanvasGroup2));
				currentLineIndex = 0;
			}
			
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

	private IEnumerator TypeSentence(string sentence)
	{
		if(playerChack.IsPlayerLeft)
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

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.V) && !isTyping && isPlayerTrigger)
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
				StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
				currentLineIndex++;
			}
		}

		if (!isPlayerTrigger && currentLineIndex != 0)
		{
			StartCoroutine(FadeOut(dialogueCanvasGroup2));
		}
	}
}

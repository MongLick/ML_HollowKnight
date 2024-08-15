using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.UI;

public class ElderbugController : MonoBehaviour
{
	[SerializeField] Animator animator;
	public Animator Animator { get { return animator; } }
	[SerializeField] LayerMask playerLayer;
	[SerializeField] float fadeDuration;
	[SerializeField] CanvasGroup dialogueCanvasGroup;
	[SerializeField] TMP_Text dialogueText;
	[SerializeField] Canvas dialogueCanvas;

	[SerializeField] string[] dialogueLines;
	[SerializeField] bool isTyping;

	private int currentLineIndex = 0;


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			StartCoroutine(FadeIn());
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			StartCoroutine(FadeOut());
		}
	}

	private IEnumerator FadeIn()
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			dialogueCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
			yield return null;
		}
		dialogueCanvasGroup.alpha = 1f;
	}

	private IEnumerator FadeOut()
	{
		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			dialogueCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
			yield return null;
		}
		dialogueCanvasGroup.alpha = 0f;
	}

	private IEnumerator TypeSentence(string sentence)
	{
		isTyping = true;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
		isTyping = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.V) && !isTyping)
		{
			currentLineIndex++;
			if (currentLineIndex < dialogueLines.Length)
			{
				StartCoroutine(TypeSentence(dialogueLines[currentLineIndex]));
			}
		}
	}
}

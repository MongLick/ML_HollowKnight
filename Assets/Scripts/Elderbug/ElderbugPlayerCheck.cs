using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderbugPlayerCheck : MonoBehaviour
{
	[SerializeField] ElderbugController elderbug;
	[SerializeField] LayerMask playerLayer;
	[SerializeField] bool isPlayerLeft;
	public bool IsPlayerLeft { get { return isPlayerLeft; } }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			elderbug.Animator.SetBool("LeftIdle", true);
			isPlayerLeft = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			elderbug.Animator.SetBool("LeftIdle", false);
			isPlayerLeft = false;
		}
	}
}

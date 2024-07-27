using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimation : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] SpriteRenderer render;

	public void PlayDustJumpAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("DustJump");
		}
	}

	public void PlayDustFallAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("DustFall");
		}
	}

	public void PlayHitAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("Hit");
		}
	}
}

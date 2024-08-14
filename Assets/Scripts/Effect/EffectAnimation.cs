using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimation : MonoBehaviour
{
	[SerializeField] PlayerController player;
	[SerializeField] Animator animator;
	[SerializeField] SpriteRenderer render;

	private void OnEnable()
	{
		if (player == null)
		{
			player = FindAnyObjectByType<PlayerController>();
		}
	}

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

	public void PlayDashAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("Dash");
			if (player.LastMoveDir.x > 0)
			{
				render.flipX = true;
			}
			else
			{
				render.flipX = false;
			}
		}
	}

	public void PlayTakeHitAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("TakeHit");
		}
	}

	public void PlayHitBloodAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("Blood");
		}
	}
}

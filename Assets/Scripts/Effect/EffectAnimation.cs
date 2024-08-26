using UnityEngine;

public class EffectAnimation : MonoBehaviour
{
	[Header("Components")]
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

	public void PlayHealAnimation()
	{
		if (animator != null)
		{
			animator.SetTrigger("Heal");
		}
	}

	public void PlaySpear()
	{
		if (animator != null)
		{
			animator.SetTrigger("Spear");
		}
	}

	public void PlayLaunch()
	{
		if (animator != null)
		{
			animator.SetTrigger("LaunchEffect");
		}
	}

	public void PlayCircular()
	{
		if (animator != null)
		{
			animator.SetTrigger("CircularEffect");
		}
	}
}

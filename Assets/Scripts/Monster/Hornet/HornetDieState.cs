using System.Collections;
using UnityEngine;
using static HornetState;

public class HornetDieState : BaseState<HornetStateType>
{
	private HornetController hornet;

	public HornetDieState(HornetController hornet)
	{
		this.hornet = hornet;
	}

	public override void Enter()
	{
		hornet.StartCoroutine(DieCoroutine());
	}

	private IEnumerator DieCoroutine()
	{
		Manager.Sound.PlaySFX(Manager.Sound.HornetDie);
		hornet.IsDie = true;
		hornet.Animator.SetBool("Die", true);
		hornet.Hornetcollider.enabled = false;
		hornet.Rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		hornet.Rigid.velocity = Vector2.zero;

		float fadeDuration = 3.0f;
		Color startColor = hornet.Render.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

		float elapsedTime = 0f;
		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.deltaTime;
			hornet.Render.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
			yield return null;
		}
		hornet.Render.color = endColor;

		yield return new WaitForSeconds(hornet.DieTime);
		hornet.gameObject.SetActive(false);
	}
}

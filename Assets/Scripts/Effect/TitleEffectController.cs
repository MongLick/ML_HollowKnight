using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEffectController : MonoBehaviour
{
	public ParticleSystem smokeParticleSystem;
	public ParticleSystem circleParticleSystem;

	private void Start()
	{
		if (smokeParticleSystem != null)
		{
			smokeParticleSystem.Play();
		}

		if (circleParticleSystem != null)
		{
			circleParticleSystem.Play();
		}
	}
}

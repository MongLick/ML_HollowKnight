using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] PlayerController player;
	[SerializeField] List<GameObject> attackObjects;
	[SerializeField] List<Vector3> attackPositions;

	private Dictionary<string, GameObject> attackEffectDictionary;
	private Dictionary<GameObject, Vector3> attackEffectPositionDictionary;

	private void Awake()
	{
		attackEffectDictionary = new Dictionary<string, GameObject>();
		attackEffectPositionDictionary = new Dictionary<GameObject, Vector3>();

		for (int i = 0; i < attackObjects.Count; i++)
		{
			var effect = attackObjects[i];
			var position = attackPositions[i];

			attackEffectDictionary.Add(effect.name, effect);
			attackEffectPositionDictionary.Add(effect, position);
			effect.SetActive(false);
		}
	}

	public void OnAttackAnimationEvent(string effectName, bool isFacingRight)
	{
		ActivateEffect(effectName, isFacingRight);
		StartCoroutine(DeactivateEffectAfterTime(effectName, 0.05f));
	}

	public void ActivateEffect(string effectName, bool shouldFlip)
	{
		if (attackEffectDictionary.TryGetValue(effectName, out GameObject effect))
		{
			effect.SetActive(true);

			Vector3 originalPosition = attackEffectPositionDictionary[effect];

			if (!shouldFlip)
			{
				effect.transform.localPosition = originalPosition;
				effect.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
			else
			{
				effect.transform.rotation = Quaternion.Euler(0, 180, 0);

				switch (effectName)
				{
					case "Attack01":
						effect.transform.localPosition = new Vector3(2, 0.1f, 0);
						break;
					case "Attack02":
						effect.transform.localPosition = new Vector3(2, -0.7f, 0);
						break;
					case "Attack03":
						effect.transform.localPosition = new Vector3(2, 0.1f, 0);
						break;
					case "Attack04":
						effect.transform.localPosition = new Vector3(0.1f, 0.5f, 0);
						break;
					case "AttackTop01":
						effect.transform.localPosition = new Vector3(0.3f, 1.2f, 0);
						break;
					case "AttackTop02":
						effect.transform.localPosition = new Vector3(-0.7f, 0.7f, 0);
						break;
					case "AttackBottom01":
						effect.transform.localPosition = new Vector3(0.1f, -1, 0);
						break;
					case "AttackBottom02":
						effect.transform.localPosition = new Vector3(-0.8f, -0.6f, 0);
						break;
				}
			}
		}
	}

	public void DeactivateEffect(string effectName)
	{
		if (attackEffectDictionary.TryGetValue(effectName, out GameObject effect))
		{
			effect.SetActive(false);
		}
	}

	private IEnumerator DeactivateEffectAfterTime(string effectName, float delay)
	{
		yield return new WaitForSeconds(delay);
		DeactivateEffect(effectName);
	}
}

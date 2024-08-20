using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] PlayerController player;
	[SerializeField] List<GameObject> attackObjects;
	[SerializeField] List<Vector3> attackPositions;
	[SerializeField] float delay;

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
		StartCoroutine(ActivateAndScheduleNextEffect(effectName, isFacingRight));
	}

	private IEnumerator ActivateAndScheduleNextEffect(string effectName, bool isFacingRight)
	{
		ActivateEffect(effectName, isFacingRight);
		yield return new WaitForSeconds(delay);
		DeactivateEffect(effectName);

		string nextEffectName = null;
		switch (effectName)
		{
			case "Attack01":
				nextEffectName = "Attack02";
				break;
			case "Attack03":
				nextEffectName = "Attack04";
				break;
			case "AttackTop01":
				nextEffectName = "AttackTop02";
				break;
			case "AttackBottom01":
				nextEffectName = "AttackBottom02";
				break;
		}

		if (!string.IsNullOrEmpty(nextEffectName))
		{
			ActivateEffect(nextEffectName, isFacingRight);
			yield return new WaitForSeconds(delay); 
			DeactivateEffect(nextEffectName);
		}
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
						effect.transform.localPosition = new Vector3(1, 0.1f, 0);
						break;
					case "Attack02":
						effect.transform.localPosition = new Vector3(1, -0.3f, 0);
						break;
					case "Attack03":
						effect.transform.localPosition = new Vector3(0.7f, 0.1f, 0);
						break;
					case "Attack04":
						effect.transform.localPosition = new Vector3(0f, 0.35f, 0);
						break;
					case "AttackTop01":
						effect.transform.localPosition = new Vector3(0.1f, 0.5f, 0);
						break;
					case "AttackTop02":
						effect.transform.localPosition = new Vector3(-0.4f, 0.2f, 0);
						break;
					case "AttackBottom01":
						effect.transform.localPosition = new Vector3(0.1f, -0.5f, 0);
						break;
					case "AttackBottom02":
						effect.transform.localPosition = new Vector3(-0.4f, -0.2f, 0);
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
}

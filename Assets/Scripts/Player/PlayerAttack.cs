using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
	[SerializeField] int damage;
	[SerializeField] PlayerController player;
	[SerializeField] List<GameObject> attackObjects;

	private Dictionary<string, GameObject> attackEffectDictionary;

	private void Awake()
	{
		attackEffectDictionary = new Dictionary<string, GameObject>();

		foreach (var effect in attackObjects)
		{
			attackEffectDictionary.Add(effect.name, effect);
			effect.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IDamageable damageable = collision.GetComponent<IDamageable>();

		if(damageable != null)
		{
			damageable.TakeDamage(damage);
		}
	}

	public void OnAttackAnimationEvent(string effectName, bool isFacingRight)
	{
		ActivateEffect(effectName, isFacingRight);
		StartCoroutine(DeactivateEffectAfterTime(effectName, 0.05f));
	}

	public void ActivateEffect(string effectName, bool isFacingRight)
	{
		if (attackEffectDictionary.TryGetValue(effectName, out GameObject effect))
		{
			effect.SetActive(true);

			Vector3 defaultPosition = effect.transform.localPosition;

			effect.transform.localPosition = isFacingRight ? new Vector3(-defaultPosition.x, defaultPosition.y, defaultPosition.z) : defaultPosition;

			Vector3 scale = effect.transform.localScale;
			scale.x = isFacingRight ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
			effect.transform.localScale = scale;
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

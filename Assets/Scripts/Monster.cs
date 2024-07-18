using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    [SerializeField] int hp;

	public void TakeDamage(int damage)
	{
		hp -= damage;
		Debug.Log(hp);
		if(hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	[SerializeField] Rigidbody2D rigid;
	public Rigidbody2D Rigid { get { return rigid; } set { rigid = value; } }
	[SerializeField] float knockbackPower;
	public float KnockbackPower { get { return knockbackPower; } }

	public void ApplyKnockback(Vector2 direction)
	{
		rigid.AddForce(direction * knockbackPower, ForceMode2D.Impulse);
	}
}

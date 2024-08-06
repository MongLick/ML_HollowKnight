using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	[SerializeField] LayerMask groundCheckLayer;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (groundCheckLayer.Contain(collision.gameObject.layer))
		{
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] LayerMask playerLayer;
	[SerializeField] float bounceForce;
	[SerializeField] private float rotationSpeed;
	[SerializeField] PooledObject pooledObject;
	[SerializeField] bool isInitialized;

	public void Initialize(Vector3 direction)
	{
		isInitialized = true;
		rigid.AddForce(direction, ForceMode2D.Impulse);
	}

	private void Update()
	{
		if (isInitialized)
		{
			transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer))
		{
			isInitialized = false;
			pooledObject.Release();
		}
	}
}

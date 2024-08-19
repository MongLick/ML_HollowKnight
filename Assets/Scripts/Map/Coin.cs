using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	private Vector2 velocity;
	public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] LayerMask playerLayer;
	[SerializeField] float bounceForce;
	[SerializeField] private float rotationSpeed;
	[SerializeField] PooledObject pooledObject;
	[SerializeField] bool isInitialized;
	[SerializeField] float maxYSpeed;

	public void Initialize(Vector3 direction)
	{
		isInitialized = true;
		rigid.AddForce(direction, ForceMode2D.Impulse);
	}

	private void FixedUpdate()
	{
		velocity = rigid.velocity;

		if (velocity.y < -maxYSpeed)
		{
			velocity.y = -maxYSpeed;
			rigid.velocity = velocity;
		}
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
			Manager.Data.GameData.Coin++;
			isInitialized = false;
			pooledObject.Release();
		}
	}
}

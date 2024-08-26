using UnityEngine;

public class Coin : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] PooledObject pooledObject;
	[SerializeField] LayerMask playerLayer;

	[Header("Vector")]
	private Vector2 velocity;
	public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

	[Header("Specs")]
	[SerializeField] float bounceForce;
	[SerializeField] float rotationSpeed;
	[SerializeField] float maxYSpeed;
	private bool isInitialized;

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
			Manager.Sound.PlaySFX(Manager.Sound.Coin);
			Manager.Data.GameData.Coin++;
			isInitialized = false;
			pooledObject.Release();
		}
	}

	public void Initialize(Vector3 direction)
	{
		isInitialized = true;
		rigid.AddForce(direction, ForceMode2D.Impulse);
	}
}

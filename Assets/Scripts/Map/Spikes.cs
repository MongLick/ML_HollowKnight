using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] LayerMask playerCheckLayer;
	[SerializeField] int damage;
	[SerializeField] float respawnDelay;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(playerCheckLayer.Contain(collision.gameObject.layer))
		{
			IDamageable damageable = collision.GetComponent<IDamageable>();
			if (damageable != null)
			{
				damageable.TakeDamage(damage);
			}

			StartCoroutine(PlayerRespawn(collision.transform));
		}
	}

	private IEnumerator PlayerRespawn(Transform playerTransform)
	{
		Manager.Game.RespawnPlayer(playerTransform);

		Rigidbody2D rigid = playerTransform.GetComponent<Rigidbody2D>();
		if (rigid != null)
		{
			rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			rigid.velocity = Vector2.zero; 
		}

		yield return new WaitForSeconds(respawnDelay);

		if (rigid != null)
		{
			rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}

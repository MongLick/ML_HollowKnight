using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GatePoint : MonoBehaviour
{
	[SerializeField] UnityEvent onDirtmouthSceneLoad;
	public UnityEvent OnDirtmouthSceneLoad { get { return onDirtmouthSceneLoad; } }
	[SerializeField] LayerMask playerLayer;
	[SerializeField] bool isGatePointTrigger;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isGatePointTrigger)
		{
			onDirtmouthSceneLoad.Invoke();
			isGatePointTrigger = true;
		}
	}
}

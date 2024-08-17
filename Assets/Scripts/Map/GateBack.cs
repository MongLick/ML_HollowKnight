using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateBack : MonoBehaviour
{
	[SerializeField] UnityEvent onKingsPassScene;
	public UnityEvent OnKingsPassScene { get { return onKingsPassScene; } }
	[SerializeField] LayerMask playerLayer;
	[SerializeField] bool isGateBackTrigger;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(playerLayer.Contain(collision.gameObject.layer) && !isGateBackTrigger)
		{
			onKingsPassScene.Invoke();
			isGateBackTrigger = true;
		}
	}
}
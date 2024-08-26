using UnityEngine;
using UnityEngine.Events;

public class GateBack : MonoBehaviour
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onKingsPassScene;
	public UnityEvent OnKingsPassScene { get { return onKingsPassScene; } set { onKingsPassScene = value; } }

	[Header("Components")]
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	private bool isGateBackTrigger;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isGateBackTrigger)
		{
			onKingsPassScene.Invoke();
			isGateBackTrigger = true;
		}
	}
}

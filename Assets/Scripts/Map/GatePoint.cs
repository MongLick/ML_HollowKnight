using UnityEngine;
using UnityEngine.Events;

public class GatePoint : MonoBehaviour
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent onDirtmouthSceneLoad;
	public UnityEvent OnDirtmouthSceneLoad { get { return onDirtmouthSceneLoad; } set { onDirtmouthSceneLoad = value; } }

	[Header("Components")]
	[SerializeField] LayerMask playerLayer;

	[Header("Specs")]
	private bool isGatePointTrigger;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (playerLayer.Contain(collision.gameObject.layer) && !isGatePointTrigger)
		{
			onDirtmouthSceneLoad.Invoke();
			isGatePointTrigger = true;
		}
	}
}

using UnityEngine;

public class InGameUI : BaseUI
{
	[Header("Components")]
	public Transform followTarget;

	[Header("Vector")]
	public Vector3 followOffset;

	private void LateUpdate()
	{
		if (followTarget != null)
		{
			transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + followOffset;
		}
	}

	public void Close()
	{
		Manager.UI.CloseInGameUI();
	}
}

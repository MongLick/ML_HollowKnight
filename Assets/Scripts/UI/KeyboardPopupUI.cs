using UnityEngine.UI;

public class KeyboardPopupUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();
		GetUI<Button>("CloseButton").onClick.AddListener(Close);
	}
}

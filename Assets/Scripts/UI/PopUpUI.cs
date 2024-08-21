public class PopUpUI : BaseUI
{
    public void Close()
    {
		Manager.Sound.PlaySFX(Manager.Sound.UiButton);
		Manager.UI.ClosePopUpUI();
    }
}

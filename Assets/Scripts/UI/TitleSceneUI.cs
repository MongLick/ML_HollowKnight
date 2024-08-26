using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneUI : PopUpUI
{
	[Header("Components")]
	[SerializeField] SettingsPopupUI settingsPopupUI;
	[SerializeField] TitleScene title;

	[Header("Specs")]
	private bool isGameStart;
	public bool IsGameStart { get { return isGameStart; } }
	private bool isButtonClick;

	protected override void Awake()
	{
		base.Awake();

		GetUI<Button>("GameStartButton").onClick.AddListener(GameStartClick);
		GetUI<Button>("OptionsButton").onClick.AddListener(OptionsClick);
		GetUI<Button>("GameEndButton").onClick.AddListener(GameEndClick);
		title = FindObjectOfType<TitleScene>();
	}

	public void ButtonClick()
	{
		Manager.Sound.PlaySFX(Manager.Sound.UiButton);
	}

	public void GameStartClick()
	{
		if (!isGameStart)
		{
			isGameStart = true;
			Manager.Sound.PlaySFX(Manager.Sound.UiButton);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			StartCoroutine(LoadingRoutine());
		}
	}

	public void OptionsClick()
	{
		ButtonClick();
		Manager.UI.ShowPopUpUI(settingsPopupUI);
		gameObject.SetActive(false);
	}

	public void GameEndClick()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public IEnumerator LoadingRoutine()
	{
		title = FindObjectOfType<TitleScene>();
		Manager.Scene.LoadFadeOut();
		yield return new WaitForSeconds(Manager.Scene.FadeTime);
		Manager.UI.VideoBack.gameObject.SetActive(true);
		Manager.Sound.StopBGM(Manager.Sound.TitleSoundClip);
		Manager.Scene.LoadFadeIn();
		title.VideoPlayer.clip = title.VideoClip1;
		isGameStart = false;
		title.VideoPlayer.Play();
	}
}

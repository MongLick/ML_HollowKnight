using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleSceneUI : PopUpUI
{
	[SerializeField] SettingsPopupUI settingsPopupUI;
	[SerializeField] bool isGameStart;
	public bool IsGameStart { get { return isGameStart; } }
	[SerializeField] bool isButtonClick;
	private TitleScene title;

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
		Manager.Scene.LoadFadeOut();
		yield return new WaitForSeconds(Manager.Scene.FadeTime);
		Manager.UI.VideoBack.gameObject.SetActive(true);
		Manager.Sound.StopBGM(Manager.Sound.TitleSoundClip);
		Manager.Scene.LoadFadeIn();
		title.VideoPlayer.clip = title.VideoClip1;
		title.VideoPlayer.Play();
	}
}

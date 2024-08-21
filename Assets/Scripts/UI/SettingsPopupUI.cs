using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopupUI : PopUpUI
{
	[SerializeField] GameObject titleSceneUI;
	[SerializeField] AudioSettingsUI audioSettingsPrefab;
	[SerializeField] KeyboardPopupUI keyboardPopupPrefab;

	protected override void Awake()
	{
		base.Awake();

		GetUI<Button>("SoundButton").onClick.AddListener(AudioSettings);
		GetUI<Button>("KeyboardButton").onClick.AddListener(KeyboardSettings);
		GetUI<Button>("CloseButton").onClick.AddListener(SettingsClose);
	}

	public void ButtonClick()
	{
		Manager.Sound.PlaySFX(Manager.Sound.UiButton);
	}

	public void AudioSettings()
	{
		ButtonClick();
		Manager.UI.ShowPopUpUI(audioSettingsPrefab);
	}

	public void KeyboardSettings()
	{
		ButtonClick();
		Manager.UI.ShowPopUpUI(keyboardPopupPrefab);
	}

	public void SettingsClose()
	{
		Close();
		titleSceneUI.gameObject.SetActive(true);
	}
}

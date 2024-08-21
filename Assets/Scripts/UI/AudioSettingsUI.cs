using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : PopUpUI
{
	[SerializeField] Slider masterVolumeSlider;
	[SerializeField] Slider bgmVolumeSlider;
	[SerializeField] Slider sfxVolumeSlider;

	protected override void Awake()
	{
		base.Awake();
		GetUI<Button>("CloseButton").onClick.AddListener(Close);
	}

	private void Start()
	{
		masterVolumeSlider.value = AudioListener.volume;
		bgmVolumeSlider.value = Manager.Sound.BGMVolme;
		sfxVolumeSlider.value = Manager.Sound.SFXVolme;

		masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
		bgmVolumeSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
		sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
	}

	private void OnMasterVolumeChanged(float value)
	{
		AudioListener.volume = value;
	}

	private void OnBGMVolumeChanged(float value)
	{
		SoundManager.Instance.BGMVolme = value;
	}

	private void OnSFXVolumeChanged(float value)
	{
		SoundManager.Instance.SFXVolme = value;
	}

}

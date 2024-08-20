using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleScene : BaseScene
{
	[SerializeField] bool isGameStart;
	[SerializeField] bool isButtonClick;
	[SerializeField] VideoPlayer videoPlayer;
	[SerializeField] VideoClip videoClip1;
	[SerializeField] VideoClip videoClip2;
	[SerializeField] Image videoBack;

	private void Start()
	{
		Cursor.visible = true;
		Manager.Sound.PlayBGM(Manager.Sound.TitleSoundClip);

		videoPlayer.loopPointReached += OnVideoClip1Ended;
	}

	public void ButtonClick()
	{
		if (!isButtonClick)
		{
			isButtonClick = true;
			Manager.Sound.PlaySFX(Manager.Sound.UiButton);
		}
	}

	public void StartButton()
	{
		if (!isGameStart)
		{
			isGameStart = true;
			Manager.Sound.PlaySFX(Manager.Sound.UiButton);

			StartCoroutine(LoadingRoutine());
		}
	}

	private void OnVideoClip1Ended(VideoPlayer vp)
	{
		videoPlayer.clip = videoClip2;
		videoPlayer.Play();

		videoPlayer.loopPointReached -= OnVideoClip1Ended;
		videoPlayer.loopPointReached += OnVideoClip2Ended;
	}

	private void OnVideoClip2Ended(VideoPlayer vp)
	{
		KingsPassSceneLoad();
		videoBack.gameObject.SetActive(false);
	}

	private void KingsPassSceneLoad()
	{
		Manager.Scene.LoadScene("KingsPassScene");
	}

	public void TitleSceneEnd()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public override IEnumerator LoadingRoutine()
	{
		Manager.Scene.LoadFadeOut();
		yield return new WaitForSeconds(Manager.Scene.FadeTime);
		Cursor.visible = false;
		videoBack.gameObject.SetActive(true);
		Manager.Sound.StopBGM(Manager.Sound.TitleSoundClip);
		Manager.Scene.LoadFadeIn();
		videoPlayer.clip = videoClip1;
		videoPlayer.Play();
	}
}

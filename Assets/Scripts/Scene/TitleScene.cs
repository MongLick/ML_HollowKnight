using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class TitleScene : BaseScene
{
	[Header("Components")]
	[SerializeField] VideoPlayer videoPlayer;
	public VideoPlayer VideoPlayer { get { return videoPlayer; } set { videoPlayer = value; } }
	[SerializeField] VideoClip videoClip1;
	public VideoClip VideoClip1 { get { return videoClip1; } set { videoClip1 = value; } }
	[SerializeField] VideoClip videoClip2;
	public VideoClip VideoClip2 { get { return videoClip2; } set { videoClip2 = value; } }

	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Manager.Sound.PlayBGM(Manager.Sound.TitleSoundClip);
		videoPlayer.targetCamera = Camera.main;
		videoPlayer.loopPointReached += OnVideoClip1Ended;

		Manager.UI.IsTitleSceneActive = true;
	}

	private void OnDisable()
	{
		if (Manager.UI != null && Manager.UI.TitleUIInstance != null)
		{
			Manager.UI.TitleUIInstance.SetActive(false);
			Manager.UI.IsTitleSceneActive = false;
		}

		if (Manager.Data != null && Manager.Data.GameData != null)
		{
			Manager.Data.GameData.Health = 5;
			Manager.Data.GameData.Coin = 0;
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
		Manager.UI.VideoBack.gameObject.SetActive(false);
	}

	private void KingsPassSceneLoad()
	{
		Manager.Scene.LoadScene("KingsPassScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

using System.Collections;
using UnityEngine;

public class DirtmouthScene : BaseScene
{
	[Header("Components")]
	[SerializeField] GateBack gateBack;
	[SerializeField] CrossroadsZone crossroadsZone;

	private void Start()
	{
		gateBack.OnKingsPassScene.AddListener(KingsPassSceneLoad);
		crossroadsZone.OnCrossroadsScene.AddListener(CrossroadsScneneLoad);
		Cursor.visible = false;
	}

	private void OnEnable()
	{
		Manager.Game.DirtmouthUpdate();
		Manager.Sound.PlayBGM(Manager.Sound.DirtmouthSoundClip);
	}

	public void KingsPassSceneLoad()
	{
		Manager.Scene.LoadScene("KingsPassScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
	}

	public void CrossroadsScneneLoad()
	{
		Manager.Scene.LoadScene("CrossroadsScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

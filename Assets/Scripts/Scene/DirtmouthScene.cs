using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtmouthScene : BaseScene
{
	[SerializeField] GateBack gateBack;
	[SerializeField] CrossroadsZone crossroadsZone;

	private void Start()
	{
		gateBack.OnKingsPassScene.AddListener(KingsPassSceneLoad);
		crossroadsZone.OnCrossroadsScene.AddListener(CrossroadsScneneLoad);
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

	private void OnEnable()
	{
		Manager.Game.DirtmouthUpdate();
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

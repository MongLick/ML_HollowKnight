using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirtmouthScene : BaseScene
{
	[SerializeField] GateBack gateBack;

	private void Start()
	{
		gateBack.OnKingsPassScene.AddListener(KingsPassSceneLoad);
	}

	public void KingsPassSceneLoad()
	{
		Manager.Scene.LoadScene("KingsPassScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
	}

	public void TitleLoadScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

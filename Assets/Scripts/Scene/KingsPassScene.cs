using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingsPassScene : BaseScene
{
	[SerializeField] Gate gate;
	[SerializeField] Image DirtmouthSceneImage;

	private void Start()
	{
		gate.OnDirtmouthUnloadScene.AddListener(DirtmouthLoadScene);
	}

	public void TitleLoadScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	public void DirtmouthLoadScene()
	{
		Manager.Scene.LoadScene("DirtmouthScene");
		DirtmouthSceneImage.gameObject.SetActive(true);
	}

	private void OnEnable()
	{
		Manager.Game.GameManagerUpdate();
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

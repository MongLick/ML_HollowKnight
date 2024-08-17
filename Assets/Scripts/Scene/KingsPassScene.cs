using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KingsPassScene : BaseScene
{
	[SerializeField] Gate gate;
	[SerializeField] Image SceneImage;
	[SerializeField] GatePoint gatePoint;

	private void Start()
	{
		gate.OnDirtmouthUnloadScene.AddListener(DirtmouthLoadScene);
		gatePoint.OnDirtmouthSceneLoad.AddListener(DirtmouthLoadScene);
		Manager.Game.Player.OnDieEvent.AddListener(PlayerDieLoadScene);
		Cursor.visible = false;
	}

	public void DirtmouthLoadScene()
	{
		Manager.Scene.LoadScene("DirtmouthScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
	}

	private void PlayerDieLoadScene()
	{
		Manager.Scene.LoadScene("KingsPassScene");
	}

	private void OnEnable()
	{
		Manager.Game.KingsPassUpdate();
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

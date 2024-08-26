using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KingsPassScene : BaseScene
{
	[Header("Components")]
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

	private void OnEnable()
	{
		Manager.Game.KingsPassUpdate();
		Manager.Sound.PlayBGM(Manager.Sound.KingsPassSoundClip);
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
		Manager.Data.GameData.Health = 5;
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

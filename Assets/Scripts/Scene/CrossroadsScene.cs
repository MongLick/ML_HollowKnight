using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadsScene : BaseScene
{
	[SerializeField] UpDestructionZone upDestructionZone;
	[SerializeField] BossZone bossZone;

	private void Start()
	{
		upDestructionZone.OnDirtmouthScene.AddListener(DestructionScneneLoad);
		bossZone.OnBossScene.AddListener(BossScneneLoad);
		Manager.Game.Player.OnDieEvent.AddListener(PlayerDieLoadScene);
		Cursor.visible = false;
	}

	public void DestructionScneneLoad()
	{
		Manager.Scene.LoadScene("DirtmouthScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
		Cursor.visible = false;
	}

	public void BossScneneLoad()
	{
		Manager.Scene.LoadScene("BossScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
		Cursor.visible = false;
	}

	private void OnEnable()
	{
		Manager.Game.CrossroadsUpdate();
		Manager.Sound.PlayBGM(Manager.Sound.CrossoradsSoundClip);
	}

	private void PlayerDieLoadScene()
	{
		Manager.Scene.LoadScene("DirtmouthScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScene : BaseScene
{
	private void Start()
	{
		Manager.Game.Player.OnDieEvent.AddListener(PlayerDieLoadScene);
		Cursor.visible = false;
	}

	private void OnEnable()
	{
		Manager.Game.BossUpdate();
		Manager.Sound.PlayBGM(Manager.Sound.BossSoundClip);
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

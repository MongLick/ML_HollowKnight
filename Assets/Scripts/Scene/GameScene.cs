using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
	public void LoadScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	private void OnEnable()
	{
		Manager.Game.UpdateMonsterColliders();
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

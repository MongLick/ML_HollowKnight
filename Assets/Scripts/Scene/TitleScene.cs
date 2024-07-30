using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	public void GameSceneLoad()
	{
		Manager.Scene.LoadScene("GameScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

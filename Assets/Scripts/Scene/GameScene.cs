using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
	public void LoadScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

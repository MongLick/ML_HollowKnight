using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	public void GameSceneLoad()
	{
		Manager.Scene.LoadScene("GameScene");
	}

	public void GameSceneEnd()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

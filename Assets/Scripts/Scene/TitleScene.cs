using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	public void KingsPassSceneLoad()
	{
		Manager.Scene.LoadScene("KingsPassScene");
	}

	public void TitleSceneEnd()
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

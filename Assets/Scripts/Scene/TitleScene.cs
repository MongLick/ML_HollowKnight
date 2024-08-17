using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	[SerializeField] bool isGameStart;

	private void Start()
	{
		Cursor.visible = true;
	}

	public void KingsPassSceneLoad()
	{
		if(!isGameStart)
		{
			Manager.Scene.LoadScene("KingsPassScene");
			isGameStart = true;
		}
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

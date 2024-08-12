using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtmouthScene : BaseScene
{
	public void KingsPassSceneLoad()
	{
		Manager.Scene.LoadScene("KingsPassScene");
	}

	public void TitleLoadScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

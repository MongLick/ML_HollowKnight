using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadsScene : BaseScene
{
	[SerializeField] UpDestructionZone upDestructionZone;

	private void Start()
	{
		upDestructionZone.OnDirtmouthScene.AddListener(DestructionScneneLoad);
	}

	public void DestructionScneneLoad()
	{
		Manager.Scene.LoadScene("DirtmouthScene");
		Manager.Scene.FadeFast.gameObject.SetActive(true);
		Manager.Scene.Loading.gameObject.SetActive(true);
	}

	private void OnEnable()
	{
		Manager.Game.CrossroadsUpdate();
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

using System.Collections;
using UnityEngine;

public class BossScene : BaseScene
{
	[Header("Components")]
	[SerializeField] EndZone endZone;

	private void Start()
	{
		endZone.OnTitleScene.AddListener(TitleScneneLoad);
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

	public void TitleScneneLoad()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	public override IEnumerator LoadingRoutine()
	{
		yield return null;
	}
}

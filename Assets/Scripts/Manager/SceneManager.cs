using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
	[SerializeField] Image fade;
	[SerializeField] float fadeTime;
	[SerializeField] float respawnFadeTime;
	[SerializeField] bool isRespawn;
	[SerializeField] UnityEvent<string> onLoadScene;
	public UnityEvent<string> OnLoadScene { get { return onLoadScene; } }
	private BaseScene curScene;
	

	public BaseScene GetCurScene()
	{
		if (curScene == null)
		{
			curScene = FindObjectOfType<BaseScene>();
		}
		return curScene;
	}

	public T GetCurScene<T>() where T : BaseScene
	{
		if (curScene == null)
		{
			curScene = FindObjectOfType<BaseScene>();
		}
		return curScene as T;
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(LoadingRoutine(sceneName));
	}

	public void LoadFadeOut()
	{
		isRespawn = true;
		fade.gameObject.SetActive(true);
		StartCoroutine(FadeOut());
	}

	public void LoadFadeIn()
	{
		StartCoroutine(FadeIn());
		fade.gameObject.SetActive(false);
		isRespawn = fade;
	}


	IEnumerator LoadingRoutine(string sceneName)
	{
		fade.gameObject.SetActive(true);
		yield return FadeOut();

		//Manager.Pool.ClearPool();
		Manager.Sound.StopSFX();
		Manager.UI.ClearPopUpUI();
		Manager.UI.ClearWindowUI();
		Manager.UI.CloseInGameUI();

		Time.timeScale = 0f;

		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		yield return oper;

		Manager.UI.EnsureEventSystem();

		BaseScene curScene = GetCurScene();

		yield return curScene.LoadingRoutine();

		Time.timeScale = 1f;
;
		onLoadScene?.Invoke(sceneName);

		yield return FadeIn();
		fade.gameObject.SetActive(false);
	}

	IEnumerator FadeOut()
	{
		float rate = 0;
		Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
		Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

		while (rate <= 1)
		{
			if (isRespawn)
			{
				rate += Time.deltaTime / respawnFadeTime;
				fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
				yield return null;
			}
			else
			{
				rate += Time.deltaTime / fadeTime;
				fade.color = Color.Lerp(fadeInColor, fadeOutColor, rate);
				yield return null;
			}
		}
	}

	IEnumerator FadeIn()
	{
		float rate = 0;
		Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
		Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

		while (rate <= 1)
		{
			if(isRespawn)
			{
				rate += Time.deltaTime / respawnFadeTime;
				fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
				yield return null;
			}
			else
			{
				rate += Time.deltaTime / fadeTime;
				fade.color = Color.Lerp(fadeOutColor, fadeInColor, rate);
				yield return null;
			}
		}
	}
}

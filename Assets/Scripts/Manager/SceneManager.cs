using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
	[SerializeField] Image fade;
	[SerializeField] Image fadeFast;
	public Image FadeFast { get { return fadeFast; } set { FadeFast = value; } }
	[SerializeField] float fadeTime;
	public float FadeTime { get { return fadeTime; } }
	[SerializeField] float respawnFadeTime;
	[SerializeField] bool isRespawn;
	public bool IsRespawn { get { return isRespawn; } }
	[SerializeField] UnityEvent<string> onLoadScene;
	public UnityEvent<string> OnLoadScene { get { return onLoadScene; } }
	[SerializeField] BaseScene curScene;
	[SerializeField] Coroutine fadeCoroutine;
	[SerializeField] Image loading;
	public Image Loading { get { return loading; } set { loading = value; } }


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
		if (fade.gameObject.activeSelf)
		{
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
			}
		}

		fadeCoroutine = StartCoroutine(FadeOut());
		fade.gameObject.SetActive(true);
		yield return fadeCoroutine;

		Manager.Game.SetPreviousScene(UnitySceneManager.GetActiveScene().name);
		Manager.UI.ClearPopUpUI();
		Manager.UI.ClearWindowUI();
		Manager.UI.CloseInGameUI();
		fadeFast.gameObject.SetActive(false);
		loading.gameObject.SetActive(false);

		Time.timeScale = 0f;

		AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
		yield return oper;

		Manager.UI.EnsureEventSystem();

		BaseScene curScene = GetCurScene();
		yield return curScene.LoadingRoutine();


		Manager.Game.OnSceneTransition(sceneName);
		Time.timeScale = 1f;
		onLoadScene?.Invoke(sceneName);

		fadeCoroutine = StartCoroutine(FadeIn());
		yield return fadeCoroutine;
		fade.gameObject.SetActive(false);
	}

	public IEnumerator FadeOut()
	{
		Debug.Log(3);
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

		if (Manager.Game.Player != null && Manager.Game.Player.IsDie)
		{
			Manager.Data.GameData.Health = 5;
		}
	}

	public IEnumerator FadeIn()
	{
		float rate = 0;
		Color fadeOutColor = new Color(fade.color.r, fade.color.g, fade.color.b, 1f);
		Color fadeInColor = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);

		while (rate <= 1)
		{
			if (isRespawn)
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

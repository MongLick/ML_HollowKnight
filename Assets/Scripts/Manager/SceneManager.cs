using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
	[Header("UnityEvent")]
	[SerializeField] UnityEvent<string> onLoadScene;
	public UnityEvent<string> OnLoadScene { get { return onLoadScene; } set { onLoadScene = value; } }

	[Header("Components")]
	[SerializeField] Image fade;
	[SerializeField] Image fadeFast;
	public Image FadeFast { get { return fadeFast; } set { FadeFast = value; } }
	[SerializeField] Image loading;
	public Image Loading { get { return loading; } set { loading = value; } }
	[SerializeField] BaseScene curScene;
	public BaseScene CurScene { get { return curScene; } }
	[SerializeField] Coroutine fadeCoroutine;

	[Header("Specs")]
	[SerializeField] float respawnFadeTime;
	[SerializeField] float fadeTime;
	public float FadeTime { get { return fadeTime; } }
	private bool isRespawn;
	public bool IsRespawn { get { return isRespawn; } }
	private bool isSceneChange;
	public bool IsSceneChange { get { return isSceneChange; } }

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
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}

		fadeCoroutine = StartCoroutine(FadeOut());
	}

	public void LoadFadeIn()
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		fadeCoroutine = StartCoroutine(FadeIn());
		fade.gameObject.SetActive(false);
		isRespawn = fade;
	}

	public IEnumerator FadeOut()
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

	private IEnumerator LoadingRoutine(string sceneName)
	{
		isSceneChange = true;
		if (fade.gameObject.activeSelf)
		{
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
			}
		}

		fadeCoroutine = StartCoroutine(FadeOut());
		fade.gameObject.SetActive(true);
		if(Manager.Game != null && Manager.Game.Player != null && !Manager.Game.Player.IsDie)
		{
			Manager.Game.Player.gameObject.SetActive(false);
		}
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

		isSceneChange = false;
		fadeCoroutine = StartCoroutine(FadeIn());
		if (Manager.Game != null && Manager.Game.Player != null && !Manager.Game.Player.IsDie)
		{
			Manager.Game.Player.gameObject.SetActive(true);
		}
		yield return fadeCoroutine;
		fade.gameObject.SetActive(false);
	}
}

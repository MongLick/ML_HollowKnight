using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	[Header("Components")]
	[SerializeField] Canvas popUpCanvas;
	[SerializeField] Canvas windowCanvas;
	[SerializeField] Canvas inGameCanvas;
	[SerializeField] Canvas videoCanvas;
	[SerializeField] GameObject titlePrefab;
	[SerializeField] GameObject titleUIInstance;
	public GameObject TitleUIInstance { get { return titleUIInstance; } set { titleUIInstance = value; } }
	[SerializeField] Stack<PopUpUI> popUpStack = new Stack<PopUpUI>();
	[SerializeField] InGameUI curInGameUI;
	[SerializeField] Image videoBack;
	public Image VideoBack { get { return videoBack; } }

	[Header("Specs")]
	private bool isTitleSceneActive;
	public bool IsTitleSceneActive { get { return isTitleSceneActive; } set { isTitleSceneActive = value; } }

	private void Start()
	{
		EnsureEventSystem();

		Camera mainCamera = Camera.main;
		popUpCanvas.worldCamera = mainCamera;
		windowCanvas.worldCamera = mainCamera;
		inGameCanvas.worldCamera = mainCamera;
		videoCanvas.worldCamera = mainCamera;

		OnTitleSceneLoad();
	}

	public void OnTitleSceneLoad()
	{
		if (titlePrefab != null)
		{
			titleUIInstance = Instantiate(titlePrefab, popUpCanvas.transform);
			titleUIInstance.SetActive(true);
		}
	}

	public void EnsureEventSystem()
	{
		if (EventSystem.current != null)
			return;

		EventSystem eventSystem = Resources.Load<EventSystem>("UI/EventSystem");
		Instantiate(eventSystem);
	}

	public T ShowPopUpUI<T>(T popUpUI) where T : PopUpUI
	{
		if (popUpStack.Count > 0)
		{
			PopUpUI topUI = popUpStack.Peek();
			topUI.gameObject.SetActive(false);
		}

		T ui = Instantiate(popUpUI, popUpCanvas.transform);
		popUpStack.Push(ui);
		return ui;
	}

	public void ClosePopUpUI()
	{
		PopUpUI ui = popUpStack.Pop();
		Destroy(ui.gameObject);

		if (popUpStack.Count > 0)
		{
			PopUpUI topUI = popUpStack.Peek();
			topUI.gameObject.SetActive(true);
		}
		else
		{
			if (isTitleSceneActive)
			{
				titleUIInstance.SetActive(true);
			}
		}
	}

	public void ClearPopUpUI()
	{
		while (popUpStack.Count > 0)
		{
			ClosePopUpUI();
		}
	}

	public T ShowWindowUI<T>(T windowUI) where T : WindowUI
	{
		return Instantiate(windowUI, windowCanvas.transform);
	}

	public void SelectWindowUI(WindowUI windowUI)
	{
		windowUI.transform.SetAsLastSibling();
	}

	public void CloseWindowUI(WindowUI windowUI)
	{
		Destroy(windowUI.gameObject);
	}

	public void ClearWindowUI()
	{
		for (int i = 0; i < windowCanvas.transform.childCount; i++)
		{
			Destroy(windowCanvas.transform.GetChild(i).gameObject);
		}
	}

	public T ShowInGameUI<T>(T inGameUI) where T : InGameUI
	{
		if (curInGameUI != null)
		{
			Destroy(curInGameUI.gameObject);
		}

		T ui = Instantiate(inGameUI, inGameCanvas.transform);
		curInGameUI = ui;
		return ui;
	}

	public void CloseInGameUI()
	{
		if (curInGameUI == null)
			return;

		Destroy(curInGameUI.gameObject);
		curInGameUI = null;
	}
}

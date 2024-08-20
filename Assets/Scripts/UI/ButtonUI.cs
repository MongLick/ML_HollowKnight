using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Image leftHighlightImage;
	[SerializeField] Image rightHighlightImage;
	[SerializeField] TitleScene titelScene;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(titelScene.IsGameStart)
		{
			return;
		}
		Manager.Sound.PlaySFX(Manager.Sound.UiButtonChange);
		ShowHighlightImages(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ShowHighlightImages(false);
	}

	private void ShowHighlightImages(bool value)
	{
		leftHighlightImage.gameObject.SetActive(value);
		rightHighlightImage.gameObject.SetActive(value);
	}
}

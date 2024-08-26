using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIView : MonoBehaviour
{
	[Header("Components")]
	[SerializeField] List<Image> healthIcons;
	[SerializeField] Sprite activeHealthSprite;
	[SerializeField] Sprite inactiveHealthSprite;
	[SerializeField] TMP_Text text;

	private void Start()
	{
		if (Manager.Data.GameData == null)
		{
			Manager.Data.NewData();
		}

		UpdateHealth(Manager.Data.GameData.Health);
		UpdateCoint(Manager.Data.GameData.Coin);
		Manager.Data.GameData.OnhealthChanged += UpdateHealth;
		Manager.Data.GameData.OnCoinChanged += UpdateCoint;
	}

	private void UpdateHealth(int health)
	{
		for (int i = 0; i < healthIcons.Count; i++)
		{
			if (healthIcons[i] == null)
			{
				continue;
			}

			if (i < health)
			{
				healthIcons[i].sprite = activeHealthSprite;
			}
			else
			{
				healthIcons[i].sprite = inactiveHealthSprite;
			}
		}
	}

	private void UpdateCoint(int value)
	{
		text.text = value.ToString();
	}
}

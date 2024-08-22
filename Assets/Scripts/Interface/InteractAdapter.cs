using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdapter : MonoBehaviour, IInteractable
{
	public UnityEvent<PlayerController> OnInteracted;

	public void Interact(PlayerController player)
	{
		OnInteracted?.Invoke(player);
	}
}

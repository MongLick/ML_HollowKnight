using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] Collider2D playerCollider;
	public Collider2D PlayerCollider { get {  return playerCollider; } }
	[SerializeField] List<Collider2D> monsterColliders = new List<Collider2D>();
	public List<Collider2D> MonsterColliders { get { return monsterColliders; } }
	[SerializeField] LayerMask monsterLayer;

	private void Start()
	{
		playerCollider = FindAnyObjectByType<PlayerController>().GetComponent<Collider2D>();

		FindMonstersInLayer();
	}

	private void FindMonstersInLayer()
	{
		var allMonsters = FindObjectsOfType<Collider2D>();

		foreach (var collider in allMonsters)
		{
			if (monsterLayer.Contain(collider.gameObject.layer))
			{
				monsterColliders.Add(collider);
			}
		}
	}
}

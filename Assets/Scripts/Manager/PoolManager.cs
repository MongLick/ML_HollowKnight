using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
	private Dictionary<int, ObjectPool> poolDic = new Dictionary<int, ObjectPool>();

	public void CreatePool(PooledObject prefab, int size, int capacity)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = $"Pool_{prefab.name}";
		DontDestroyOnLoad(gameObject);

		ObjectPool objectPool = gameObject.AddComponent<ObjectPool>();
		objectPool.CreatePool(prefab, size, capacity);

		poolDic.Add(prefab.GetInstanceID(), objectPool);
	}

	public void DestroyPool(PooledObject prefab)
	{
		ObjectPool objectPool = poolDic[prefab.GetInstanceID()];
		Destroy(objectPool.gameObject);

		poolDic.Remove(prefab.GetInstanceID());
	}

	public PooledObject GetPool(PooledObject prefab, Vector3 position, Quaternion rotation)
	{
		return poolDic[prefab.GetInstanceID()].GetPool(position, rotation);
	}
}

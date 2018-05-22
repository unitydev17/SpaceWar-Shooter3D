﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class MasterAI : MonoBehaviour
{

	[SerializeField]
	private int MAX_ENEMIES_COUNT = 1;

	[SerializeField]
	private float SPAWN_TIME = 5f;

	public GameObject enemyPrefab;

	public GameObject spawnPointObj;
	public GameObject defenceObj;


	private List<GameObject> items;


	void Awake()
	{
		items = new List<GameObject>();
	}


	void Start()
	{
		InvokeRepeating("SpawnEnemies", 0, SPAWN_TIME);
	}


	void SpawnEnemies()
	{
		if (items.Count >= MAX_ENEMIES_COUNT) {
			return;
		}
		GameObject item = Instantiate(enemyPrefab, spawnPointObj.transform.position, Quaternion.identity);
		item.transform.parent = GameController.root.transform;
		items.Add(item);

		var itemAI = item.GetComponent<ItemAI> ();
		itemAI.SetMasterAI (this);
		itemAI.SetStrategy(new DefenceStrategy(this, item.transform, defenceObj.transform));

	}


	public void RemoveEnemy(GameObject gameObject)
	{
		if (items.Count > 0) {
			items.Remove(gameObject);
		}
	}

}
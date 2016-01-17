using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{
	public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
	public float zMaxRange = 25.0f;
	public GameObject[] spawnObjects;

	private float nextSpawnTime;

	void Start ()
	{
		nextSpawnTime = Time.time+secondsBetweenSpawning;
	}
	
	void Update ()
	{
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		if (Time.time  >= nextSpawnTime) {
            SpawningObjects();
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}	
	}

	void SpawningObjects ()
	{
		Vector3 spawnPosition;

		spawnPosition.x = Random.Range (xMinRange, xMaxRange);
		spawnPosition.y = Random.Range (yMinRange, yMaxRange);
		spawnPosition.z = Random.Range (zMinRange, zMaxRange);

		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		spawnedObject.transform.parent = gameObject.transform;
	}
}

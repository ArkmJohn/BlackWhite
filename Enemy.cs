using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	

	// Array to store the spawn point game objects
	public GameObject[] spawnPoints = new GameObject[1];

	// Grunt enemy prefab
	public GameObject gEnemyPrefab;

	// Commander enemy prefab
	public GameObject cEnemyPrefab; 

	// To store the random number 
	int pos;

	// Use this for initialization
	void Start () 
	{
		//AI_Wander wanderObj = GameObject.FindGameObjectWithTag ("GruntEnemy").GetComponent<AI_Wander> ();
			
		//wanderObj.hasSpawned = false;
		// Generating a random number and storing the value in pos
		pos = Random.Range (0, 4);
		
		// Instantiating the grunt enemy 
		Instantiate(gEnemyPrefab, spawnPoints [pos].transform.position,Quaternion.identity);
		//wanderObj.hasSpawned = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour
{
	// Vector3 variable that stores the enemy position and player position
	public Vector3 playerPos;
	public Vector3 enemyPos;

	// Reference variable to the NavMeshAgent 
	public NavMeshAgent enemyAgent;

	// Variable for the enemy's speed
	public float enemySpeed;

	// To check if the enemy has to start following the player
	public bool followPlayer;

	// GameObject reference for the player game object
	GameObject playerGO;

	// Object Reference to EvadePlayer Script
	EvadePlayer evadePlayerObj;

	// Use this for initialization
	void Start ()
	{
		// Getting the player component and storing it in the variable
		playerGO = GameObject.FindGameObjectWithTag ("player");

		// Setting the object value to get the Grunt Enemy gameObject with IsLowHealth Script
		evadePlayerObj = GameObject.FindGameObjectWithTag ("GruntEnemy").GetComponent<EvadePlayer> ();

		// Setting the NavMeshAgent speed to enemy speed
		enemyAgent.speed = enemySpeed;

	}
	
	// Update is called once per frame
	void Update () 
	{
		// Setting the position value of the player
		playerPos = playerGO.transform.position;

		// If the value passed is 'true', then make the enemy to follow the player
		if(followPlayer)
		{
			// Disabling the AI_Wander Script
			gameObject.GetComponent<Wander>().enabled = false;

			// Setting the enemy position as the player position
			enemyPos = playerPos;

			// Setting the enemy desitination
			enemyAgent.SetDestination (enemyPos);
		}
		else
		{
			evadePlayerObj.ToEvade ();
		}
	}

	// Function to set the boolean 'followPlayer' value to true or false.
	public void CheckPlayerInRange(bool inRange)
	{
		// If inRange value is 'true'
		if (inRange) 
		{
			// Set the boolean value to 'true'
			followPlayer = true;
		} 
		// If value passed is 'false'
		else if (!inRange)
		{
			// Set the boolean value to 'false'
			followPlayer = false;
		}
	}
}

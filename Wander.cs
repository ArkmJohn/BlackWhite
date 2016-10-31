using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour 
{
	// Variable for the enemy's speed
	public float enemySpeed;

	// Boolean values
	public bool hasSpawned;
	public bool isWandering = false;

	// Reference variable to the NavMeshAgent 
	public NavMeshAgent enemyAgent;

	// Vector3 variable that stores the target position and player position
	public Vector3 targetPos;
	public Vector3 enemyPos;

	// Use this for initialization
	void Start () 
	{
		// Getting the NavMeshAgent component and storing it in a variable
		enemyAgent = GetComponent <NavMeshAgent> ();

		// Setting the NavMeshAgent speed to enemy speed
		enemyAgent.speed = enemySpeed;

		// Go to SetTargetPosition function
		SetTargetPosition ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check if the enemy has started wandering
		if (isWandering) 
		{
			// Assigning the current enemy position value
			enemyPos = this.gameObject.transform.position;

			// If the enemy position and the target position are not equal then proceed
			if (enemyPos != targetPos)
			{
				// Sets or updates the destination thus triggering the calculation for a new path (https://docs.unity3d.com/ScriptReference/NavMeshAgent.SetDestination.html)
				enemyAgent.SetDestination (targetPos);

				// Check if the x and z axis of the enemy and target are equal
				if(enemyPos.x == targetPos.x && enemyPos.z == targetPos.z)
				{
					// If the above condition is true then set isWandering to false
					isWandering = false;
					// Call the SetTargetPostion() to set a new target position
					SetTargetPosition ();
				}
			} 
		} 
	}

	// Function to set the new target position
	void SetTargetPosition()
	{
		// Assigning a random Vector3 value to the target position
		targetPos = new Vector3 (Random.Range(-4,14), 0.5f, Random.Range(-4,4));

		isWandering = true;
	}
		
}

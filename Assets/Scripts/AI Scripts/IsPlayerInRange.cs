using UnityEngine;
using System.Collections;

public class IsPlayerInRange : MonoBehaviour 
{
	// Object Reference to MoveTowardsPlayer Script
	MoveTowardsPlayer moveObj;

	// Object Reference to IsLowHealth Script
	IsLowHealth lowHealthObj;

	// Length of the ray
	public int rayLength;

	// Structure used to get information back from a raycast.
	public RaycastHit hit;

	void Start()
	{
		// Setting the object value to get the Grunt Enemy gameObject with MoveTowardsPlayer Script
		moveObj = GameObject.FindGameObjectWithTag ("GruntEnemy").GetComponent<MoveTowardsPlayer> ();

		// Setting the object value to get the Grunt Enemy gameObject with IsLowHealth Script
		lowHealthObj = GameObject.FindGameObjectWithTag ("GruntEnemy").GetComponent<IsLowHealth> ();
	}

	void Update()
	{
		// The direction for the ray
		Vector3 direction = transform.TransformDirection(Vector3.forward);

		// Displaying the ray in the desired direction and position
		Debug.DrawRay(transform.position, direction* rayLength, Color.green);

		// If the health of the enemy is greater than 5
		if (lowHealthObj.enemyHealth >= 5) 
		{
		// Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the scene.
		if (Physics.Raycast(transform.position, direction, out hit, rayLength))
		{
			// Checking if the collider of the gameobject hit is the "Player"
				if (hit.collider.gameObject.name == "Player") 
				{
					// Pass 'true' value to the function in MoveTowardsPlayer Script
					moveObj.CheckPlayerInRange (true);
				}
			}	
		}
		// If the enemy's health is less than 5
		else if (lowHealthObj.enemyHealth < 5)
		{
			// Pass 'false' value to the function in MoveTowardsPlayer Script
			moveObj.CheckPlayerInRange (false);
		}
	}
}


using UnityEngine;
using System.Collections;

public class AttackPlayer : MonoBehaviour 
{
	// Object Reference to PlayerMovement Script
	PlayerMovement playerObj;

	// Length of the ray
	public int rayLength;

	// Structure used to get information back from a raycast.
	public RaycastHit hit;

	Vector3 direction;

	// Use this for initialization
	void Start () 
	{
		// Setting the object value to get the Grunt Enemy gameObject with PlayerMovement Script
		playerObj = GameObject.FindGameObjectWithTag ("player").GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		// The direction for the ray
		direction = transform.TransformDirection(Vector3.forward);

		// Displaying the ray in the desired direction and position
		Debug.DrawRay(transform.position, direction * rayLength, Color.red);

		// Casts a ray, from point origin, in direction direction, of length maxDistance, against all colliders in the scene.
		if (Physics.Raycast(transform.position, direction, out hit, rayLength))
		{
			// Checking if the collider of the gameobject hit is the "Player"
			if (hit.collider.gameObject.name == "Player") 
			{
				// Start the coroutines
				StartCoroutine (DelayAttack ());
			}
		}	

		// If the player's health is less than/ equal to 0, then set it as zero.
		if (playerObj.playerHealth <= 0)
		{
			playerObj.playerHealth = 0;
		}
	}

	public IEnumerator DelayAttack() 
	{
		rayLength = 0;
		// Displaying the ray in the desired direction and position
		Debug.DrawRay(transform.position, direction * rayLength, Color.red);
		playerObj.playerHealth -= 1;
		yield return new WaitForSeconds(2.0f);
		rayLength = 1;
			
		}

}

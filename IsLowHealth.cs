using UnityEngine;
using System.Collections;

public class IsLowHealth : MonoBehaviour 
{
	// Enemy health variable
	public int enemyHealth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// This can be removed as this was inserted for reducing and increasing the health.
		// Reduce health of enemy by pressing the 'H' key
		if(Input.GetKeyDown(KeyCode.H))
		{
			if(enemyHealth <= 0)
			{
				enemyHealth = 0;
			}
			else
			enemyHealth -= 1;
		}

		// Increase enemy health by pressing the 'G' key
		if(Input.GetKeyDown(KeyCode.G))
		{
			if(enemyHealth >= 10)
			{
				enemyHealth = 10;
			}
			else
			enemyHealth += 1;
		}
		// Code can be removed till here




	}
}

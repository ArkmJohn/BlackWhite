using UnityEngine;
using System.Collections;

public class EvadePlayer : MonoBehaviour 
{
	public void ToEvade()
	{
		// If the value passed is false, then enable the AI_Wander script, so the enemy starts to wander or escape
		gameObject.GetComponent<Wander>().enabled = true;
	}
}

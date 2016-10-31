using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public NavMeshAgent playerAgent;

	public int playerHealth;

	// Use this for initialization
	void Start () 
	{
		playerAgent = GetComponent <NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (h, 0f, v);

		playerAgent.Move (movement * 5f * Time.deltaTime);
	}
}

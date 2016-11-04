using UnityEngine;
using System.Collections;

public class PauseManager: MonoBehaviour 
{
	public bool pause;
	public GameObject pausePanel;

	// Use this for initialization
	void Start () 
	{
		// Set the pause panel to false
		pausePanel.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			if (!pause) 
			{
				pause = true;
				pausePanel.SetActive (true);
				Time.timeScale = 0f;
			} 

			else if (pause) 
			{
				pause = false;
				pausePanel.SetActive (false);
				Time.timeScale = 1f;
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour {

    Player p;
    public GameObject camera, mainCamera;
    public Text numVit, numEnd, numStr, numDex, numRes, numInt;
	// Use this for initialization
	void Start () {
        p = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateText();
	}

    void UpdateText()
    {
        numVit.text = p.statistics[0].ToString();
        numEnd.text = p.statistics[1].ToString();
        numStr.text = p.statistics[2].ToString();
        numDex.text = p.statistics[3].ToString();
        numRes.text = p.statistics[4].ToString();
        numInt.text = p.statistics[5].ToString();
    }
    public void Paused()
    {
        if (Time.timeScale != 0)
        {
            mainCamera.SetActive(false);
            camera.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            mainCamera.SetActive(true);
            camera.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

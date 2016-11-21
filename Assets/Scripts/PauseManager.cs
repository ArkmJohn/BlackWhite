using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour {

    Player p;
    CameraManager cm;
    public GameObject canvas;
    public Text numVit, numEnd, numStr, numDex, numRes, numInt;
	// Use this for initialization
	void InitializePause () {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cm = FindObjectOfType<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (p == null)
            InitializePause();
        else
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
            cm.switchCamPos(2);
            canvas.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            cm.switchCamPos(1);
            canvas.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

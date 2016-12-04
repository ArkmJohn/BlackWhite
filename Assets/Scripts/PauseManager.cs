using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour {

    Player p;
    public GameObject mainCanvas, pauseButton;
    public Text numVit, numEnd, numStr, numDex, numRes, numInt;
	
	// Update is called once per frame
	void Update () {
        if(p == null)
            p = GameObject.FindObjectOfType<CharacterControl>().GetComponent<Player>();
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
            FindObjectOfType<CameraManager>().switchCamPos(2);
            pauseButton.SetActive(false);
            mainCanvas.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {
            FindObjectOfType<CameraManager>().switchCamPos(1);
            pauseButton.SetActive(true);
            mainCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }
}

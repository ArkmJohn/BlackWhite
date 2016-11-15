using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameManager : MonoBehaviour {

    public GameObject EndWinCamera, EndLostCamera, MainCamera;
    public Text level;
    public GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void LoseGame()
    {
        // Stop Time
        Time.timeScale = 0;

        // Load Camera
        EndLostCamera.SetActive(true);
        MainCamera.SetActive(false);
    }

    public void WinGame()
    {
        // Stop The Time
        Time.timeScale = 0;

        // Change the text
        EndWinCamera.SetActive(true);
        MainCamera.SetActive(false);
        level.text = "Lvl " + gm.level + " Passed!";
    }
}

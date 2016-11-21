using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameManager : MonoBehaviour {

    public GameObject EndWinCamera, EndLostCamera, MainCamera;
    public GameObject inventory;
    public Text level;
    public GameManager gm;
    public ButtonManager bm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        bm = FindObjectOfType<ButtonManager>();
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
        gm.actPlayerObj.transform.SetParent(gm.transform);
        gm.actInventObj.transform.SetParent(gm.transform);

        // Stop The Time
        Time.timeScale = 0;

        // Change the text
        EndWinCamera.SetActive(true);
        MainCamera.SetActive(false);
        level.text = "Lvl " + gm.level + " Passed!";
    }

    public void AdvanceToNextLevel()
    {
        Time.timeScale = 1;
        Debug.Log("Hello!");
        bm.LoadLevel("LevelScene");
        gm.AdvanceNextLevel();
    }

    public void GoHome()
    {
        Time.timeScale = 1;
        gm.gameObject.GetComponent<ButtonManager>().LoadLevel("Main Menu");
    }
}

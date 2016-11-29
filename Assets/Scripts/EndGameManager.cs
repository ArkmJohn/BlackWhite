using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameManager : MonoBehaviour {

    public GameObject EndWCanvas, EndLCanvas;
    public GameObject inventory;
    public Text level;
    public GameManager gm;
    public ButtonManager bm;
    public CameraManager cm;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        bm = FindObjectOfType<ButtonManager>();
        cm = FindObjectOfType<CameraManager>();
    }

    public void LoseGame()
    {
        // Stop Time
        Time.timeScale = 0;

        // Load Camera
        cm.switchCamPos(3);

        // Turn On Canvas
        EndLCanvas.SetActive(true);
    }

    public void WinGame()
    {
        gm.actPlayerObj.transform.SetParent(gm.transform);
        gm.actInventObj.transform.SetParent(gm.transform);

        // Stop The Time
        Time.timeScale = 0;

        // Turn On panel
        EndWCanvas.SetActive(true);

        // Change the text
        level.text = "Lvl " + gm.level + " Passed!";
        gm.FinishLevel();
    }

    public void AdvanceToNextLevel()
    {
        Time.timeScale = 1;
        bm.LoadLevel("LevelScene");
        //gm.AdvanceNextLevel();
    }

    public void GoHome()
    {
        Time.timeScale = 1;
        gm.EndGame();
        gm.gameObject.GetComponent<ButtonManager>().LoadLevel("Main Menu");
    }
}

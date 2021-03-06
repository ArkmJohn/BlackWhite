﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void PlayGame()
    {
        FindObjectOfType<GameManager>().InitializeLevel();
    }

    public void ChangeDifficulty(int difficultyID)
    {
        gameObject.GetComponent<GameManager>().difficulty = difficultyID;
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

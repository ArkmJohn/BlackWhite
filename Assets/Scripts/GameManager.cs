using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public LevelGenerator levelGen = null;
    public Rooms rooms = null;
    public List<GameObject> enemyPrefabs;
    public GameObject playerPrefab;

    public int level;
    public int difficulty;

    void Awake()
    {
        // Singleton purposes XD
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void InitializeLevel()
    {
        levelGen = FindObjectOfType<LevelGenerator>();
        levelGen.InitLevel();
        rooms = FindObjectOfType<Rooms>();

        rooms.SpawnEnemies(GetEnemyCount(), enemyPrefabs, playerPrefab);

        Player playerScript = GetComponent<Player>();

        playerPrefab.GetComponent<Player>().InStats(playerScript.statistics[2],
            playerScript.statistics[0],
            playerScript.statistics[3],
            playerScript.statistics[1],
            playerScript.statistics[4],
            playerScript.statistics[5]);
    }

    int GetEnemyCount()
    {
        return level + 10 + difficulty * 2;
    }

    void FinishLevel()
    {
        level++;
    }
}

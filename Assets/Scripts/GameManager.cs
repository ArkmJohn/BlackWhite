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

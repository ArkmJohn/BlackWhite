using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public LevelGenerator levelGen = null;
    public Rooms rooms = null;
    public List<GameObject> enemyPrefabs, itemPrefabs;
    public GameObject playerPrefab, startWeaponPrefab;
    public GameObject inventoryPrefab;
    public int level;
    public int hairColorIndex;
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

    public void SetHairIndex(int index)
    {
        hairColorIndex = index;
    }

    public void InitializeLevel()
    {
        levelGen = FindObjectOfType<LevelGenerator>();
        levelGen.InitLevel();
        rooms = FindObjectOfType<Rooms>();

        Player playerScript = GetComponent<Player>();

        playerPrefab.GetComponent<Player>().InStats(playerScript.statistics[2],
            playerScript.statistics[0],
            playerScript.statistics[3],
            playerScript.statistics[1],
            playerScript.statistics[4],
            playerScript.statistics[5]);

        GameObject inv = Instantiate(inventoryPrefab);
        Inventory myInv = inv.GetComponentInChildren<Inventory>();
        playerPrefab.GetComponent<CharacterControl>().inventory = myInv;
        playerPrefab.GetComponent<CharacterControl>().SetHair(hairColorIndex - 1);
        rooms.SpawnEnemies(GetEnemyCount(), enemyPrefabs, playerPrefab, itemPrefabs, startWeaponPrefab);

    }

    int GetEnemyCount()
    {
        return level + 10 + difficulty * 2;
    }

    public void AdvanceNextLevel()
    {
        


    }

    public void FinishLevel()
    {
        level++; 
    }
}

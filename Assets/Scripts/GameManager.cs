using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public LevelGenerator levelGen = null;
    public Rooms rooms = null;
    public List<GameObject> enemyPrefabs, itemPrefabs;
    public GameObject playerPrefab, startWeaponPrefab, endGoal;
    public GameObject inventoryPrefab;
    public GameObject actPlayerObj, actInventObj;
    public int level;
    public int hairColorIndex;
    public int difficulty, enemyCountInc = 2, difficultyMult = 2;

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
        actInventObj = inv;
        rooms.SpawnEndGoal(endGoal);
    }

    int GetEnemyCount()
    {
        return level + enemyCountInc + difficulty * difficultyMult;
    }

    public void AdvanceNextLevel()
    {
        
        levelGen = FindObjectOfType<LevelGenerator>();
        levelGen.InitLevel();
        rooms = FindObjectOfType<Rooms>();
        rooms.SpawnEnemiesO(GetEnemyCount(), enemyPrefabs, itemPrefabs, actPlayerObj);

        Player playerScript = actPlayerObj.GetComponent<Player>();
        CharacterControl playerCharacter = actPlayerObj.GetComponent<CharacterControl>();
        actInventObj.transform.parent = null;
        actPlayerObj.transform.parent = null;
        playerScript.gameObject.GetComponent<CameraManager>().switchCamPos(1);
        rooms.SpawnEndGoal(endGoal);

    }

    public void FinishLevel()
    {
        level++; 
    }
}

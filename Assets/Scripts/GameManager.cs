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
        inv.GetComponentInChildren<Inventory>().CreateSlots();
        Inventory myInv = inv.GetComponentInChildren<Inventory>();
        playerPrefab.GetComponent<CharacterControl>().inventory = myInv;
        playerPrefab.GetComponent<CharacterControl>().SetHair(hairColorIndex - 1);
        rooms.SpawnEnemies(GetEnemyCount(), enemyPrefabs, playerPrefab, itemPrefabs, startWeaponPrefab);
        actInventObj = inv;
        rooms.SpawnEndGoal(endGoal);
    }

    public void SaveStat(Player myP)
    {
        Player gmPScript = GetComponent<Player>();
        gmPScript.InStats(myP.statistics[2],
            myP.statistics[0],
            myP.statistics[3],
            myP.statistics[1],
            myP.statistics[4],
            myP.statistics[5]);
    }

    int GetEnemyCount()
    {
        return level + enemyCountInc + difficulty * difficultyMult;
    }

    public void AdvanceNextLevel()
    {
        //FinishLevel();
        levelGen = FindObjectOfType<LevelGenerator>();
        levelGen.InitLevel();
        rooms = FindObjectOfType<Rooms>();
        actInventObj.transform.parent = null;
        actPlayerObj.transform.parent = null;
        rooms.SpawnEnemiesO(GetEnemyCount(), enemyPrefabs, itemPrefabs, actPlayerObj);
        //actInventObj.SetActive(false);
        //actPlayerObj.SetActive(false);
        FindObjectOfType<CameraManager>().switchCamPos(1);
        //actInventObj.transform.position = Vector3.zero;
        rooms.SpawnEndGoal(endGoal);
    }

    public void FinishLevel()
    {
        level++; 
    }
}

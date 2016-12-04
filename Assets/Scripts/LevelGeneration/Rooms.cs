using UnityEngine;
using System.Collections.Generic;

public class Rooms : MonoBehaviour {

    public CaveRoom[] rooms;
    public GameObject[] missingPo;

    public void FindRooms()
    {
        rooms = GetComponentsInChildren<CaveRoom>();
    }

    public void SpawnStairs(GameObject StairsPrefab)
    {
        rooms[Random.Range(0, rooms.Length)].SpawnObjectAtRandomPosition(StairsPrefab);
    }

    public void SpawnEnemies(int enemyCount, List<GameObject> enemyPrefabs, GameObject playerPrefab, List<GameObject> itemPrefabs, GameObject startWeapon)
    {
        SpawnPlayer(Random.Range(0, rooms.Length), playerPrefab, startWeapon);

        int x = 0;
        Debug.Log("Spawned " + enemyCount + " enemies");

        while (x < 1) //enemyCount)
        {
            SpawnAnObject(Random.Range(0, rooms.Length), enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            x++;
        }

        int y = 0;
        while (y <= enemyCount - 5)
        {
            SpawnAnObject(Random.Range(0, rooms.Length), itemPrefabs[Random.Range(0, itemPrefabs.Count)]);
            y++;
        }


    }

    public void SpawnEnemiesO(int enemyCount, List<GameObject> enemyPrefabs, List<GameObject> itemPrefabs, GameObject player)
    {
        PlaceObject(Random.Range(0, rooms.Length), player);
        int x = 0;
        Debug.Log("Spawned " + enemyCount + " enemies");
        while (x < enemyCount)
        {
            SpawnAnObject(Random.Range(0, rooms.Length), enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            x++;
        }

        int y = 0;
        while (y <= enemyCount - 5)
        {
            SpawnAnObject(Random.Range(0, rooms.Length), itemPrefabs[Random.Range(0, itemPrefabs.Count)]);
            y++;
        }
        
        foreach (float a in player.GetComponent<Player>().statistics)
        {
            Debug.Log(a);
        }
    }

    void PlaceObject(int roomID, GameObject obj)
    {
        rooms[roomID].PlaceObject(obj);
    }

    void SpawnAnObject(int roomID, GameObject obj)
    {

        rooms[roomID].SpawnObjectAtRandomPosition(obj);
    }

    void SpawnPlayer(int roomID, GameObject playerPrefab, GameObject weaponPrefab)
    {
        rooms[roomID].SpawnObjectAtRandomPosition(playerPrefab, weaponPrefab);

    }

    public void SpawnEndGoal( GameObject exitPrefab)
    {
        int roomId = Random.Range(0, rooms.Length);
        rooms[roomId].SpawnObjectAtRandomPosition(exitPrefab);
    }
}

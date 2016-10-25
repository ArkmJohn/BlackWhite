using UnityEngine;
using System.Collections.Generic;

public class Rooms : MonoBehaviour {

    public CaveRoom[] rooms;

    public void FindRooms()
    {
        rooms = GetComponentsInChildren<CaveRoom>();
    }

    public void SpawnStairs(GameObject StairsPrefab)
    {
        rooms[Random.Range(0, rooms.Length)].SpawnEnemyAtRandomPosition(StairsPrefab);
    }

    public void SpawnEnemies(int enemyCount, List<GameObject> enemyPrefabs, GameObject playerPrefab)
    {
        // TODO: Choose a prefab based on percentage

        int x = 0;

        while (x < enemyCount)
        {
            SpawnAnEnemy(Random.Range(0, rooms.Length), enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            x++;
        }
        SpawnPlayer(Random.Range(0, rooms.Length), playerPrefab);

    }

    void SpawnAnEnemy(int roomID, GameObject enemyObject)
    {
        rooms[roomID].SpawnEnemyAtRandomPosition(enemyObject);
    }

    void SpawnPlayer(int roomID, GameObject playerPrefab)
    {
        rooms[roomID].SpawnEnemyAtRandomPosition(playerPrefab);
    }
}

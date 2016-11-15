﻿using UnityEngine;
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
        // TODO: Choose a prefab based on percentage

        int x = 0;

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

        SpawnPlayer(Random.Range(0, rooms.Length), playerPrefab, startWeapon);

    }

    void SpawnAnObject(int roomID, GameObject obj)
    {
        rooms[roomID].SpawnObjectAtRandomPosition(obj);
    }

    void SpawnPlayer(int roomID, GameObject playerPrefab, GameObject weaponPrefab)
    {
        rooms[roomID].SpawnObjectAtRandomPosition(playerPrefab, weaponPrefab);
    }

    public void SpawnEndGoal(int roomID, GameObject exitPrefab)
    {
        rooms[roomID].SpawnObjectAtRandomPosition(exitPrefab);
    }
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// This is made by using Cellular Automaton which is also used in the game/simulation The game of Life by John Conway
/// but with different rules to stabalize and make a cave system that is generated by using a seed and hashing it.
/// </summary>

public class LevelGenerator : MonoBehaviour
{

    public int width;
    public int height;
    public int wallThresholdSize = 50;
    public int roomThresholdSize = 50;

    public string seed;
    public bool useRandomSeed, testing, hasGenerated = false;

    public GameObject[] enemyPrefabs;
    public GameObject playerPrefab;
    public List<Vector3> availablePositions;

    [Range(0, 100)]
    public int enemyCount;
    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    List<GameObject> objectClones = new List<GameObject>();
    //GameObject myCamera;

    void Awake()
    {
        //FindObjectOfType<GameManager>().InitializeLevel();
    }
    
    void Start()
    {
        if (testing == true)
            InitLevel();
    }


    public void InitLevel()
    {
        //myCamera = GameObject.FindGameObjectWithTag("MainCamera");
        GenerateMap();
    }

    [ContextMenu("Generate Map")]
    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

        ProcessMap();

        // Adds a border wall around the map
        int borderSize = 1;
        int[,] borderedMap = new int[width + borderSize * 2, height + borderSize * 2];

        for (int x = 0; x < borderedMap.GetLength(0); x++)
        {
            for (int y = 0; y < borderedMap.GetLength(1); y++)
            {
                if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize)
                {
                    borderedMap[x, y] = map[x - borderSize, y - borderSize];
                }
                else
                {
                    borderedMap[x, y] = 1;
                }
            }
        }

        MeshGenerator meshGen = GetComponent<MeshGenerator>();
        meshGen.GenerateMesh(borderedMap, 1);
        // GetAllAvailableCoordinates(); //Already doing on smooth map
        // SpawnObjects();
        hasGenerated = true;
    }

    void ProcessMap() // Removes unwanted clutter and finds rooms
    {
        List<List<Coord>> wallRegions = GetRegions(1);
        foreach (List<Coord> wallRegion in wallRegions) // Removes unwanted small group of walls
        {
            if (wallRegion.Count < wallThresholdSize)
            {
                foreach (Coord tile in wallRegion)
                {
                    map[tile.tileX, tile.tileY] = 0;
                }
            }
        }

        List<List<Coord>> roomRegions = GetRegions(0);
        List<Room> survivingRooms = new List<Room>();

        // TODO: Create a seperate room class that contains all points for spawning enemies and the player
        foreach (List<Coord> roomRegion in roomRegions)
        {
            if (roomRegion.Count < roomThresholdSize) // Removes unwanted small rooms
            {
                foreach (Coord tile in roomRegion)
                {
                    map[tile.tileX, tile.tileY] = 1;
                }
            }
            else // Adds the surviving room to a list
            {
                survivingRooms.Add(new Room(roomRegion, map));
                Debug.Log("Added a Room");
            }
        }
        survivingRooms.Sort();
        survivingRooms[0].isMainRoom = true;
        survivingRooms[0].isAccessibleFromMainRoom = true;
        CreateCaveRoom(survivingRooms);
        ConnectClosestRooms(survivingRooms);
    }

    void CreateCaveRoom(List<Room> rooms)
    {
        GameObject roomList = new GameObject("RoomList");
        roomList.AddComponent<Rooms>();
        foreach (Room a in rooms)
        {
            GameObject room = new GameObject("Room");
            room.transform.SetParent(roomList.transform);
            room.AddComponent<CaveRoom>();
            CaveRoom roomScript = room.GetComponent<CaveRoom>();

            List<Coord> tilePos = a.tiles;
            int x = 0;
            int y = 0;
            foreach (Coord z in tilePos)
            {
                x = z.tileX;
                y = z.tileY;

                int neighbouringTiles = GetSurroundingWallCount(x, y);

                // For overhead of bigger objects
                if (neighbouringTiles < 1)
                    roomScript.addPosition(new Vector3(-width / 2 + 0.5f + x, 2, -height / 2 + 0.5f + y));

            }
            roomList.GetComponent<Rooms>().FindRooms();
            room.transform.position = roomScript.findCenter();
            x = 0;
            y = 0;
            
        }
    }

    void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false) // Finds all rooms and connect possible connections
    {
        List<Room> roomListA = new List<Room>();
        List<Room> roomListB = new List<Room>();

        // Force connects all the rooms that ar not connected
        if (forceAccessibilityFromMainRoom)
        {
            foreach (Room room in allRooms)
            {
                if (room.isAccessibleFromMainRoom)
                {
                    roomListB.Add(room);
                }
                else
                {
                    roomListA.Add(room);
                }
            }
        }
        else
        {
            roomListA = allRooms;
            roomListB = allRooms;
        }

        int bestDistance = 0;
        Coord bestTileA = new Coord();
        Coord bestTileB = new Coord();
        Room bestRoomA = new Room();
        Room bestRoomB = new Room();
        bool possibleConnectionFound = false;

        foreach (Room roomA in roomListA)
        {
            if (!forceAccessibilityFromMainRoom)
            {
                possibleConnectionFound = false;
                if (roomA.connectedRooms.Count > 0)
                {
                    continue;
                }
            }

            foreach (Room roomB in roomListB)
            {
                if (roomA == roomB || roomA.IsConnected(roomB))
                {
                    continue;
                }
                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
                {
                    for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
                    {
                        Coord tileA = roomA.edgeTiles[tileIndexA];
                        Coord tileB = roomB.edgeTiles[tileIndexB];
                        int distanceBetweenRooms = (int)(Mathf.Pow(tileA.tileX - tileB.tileX, 2) + Mathf.Pow(tileA.tileY - tileB.tileY, 2));

                        if (distanceBetweenRooms < bestDistance || !possibleConnectionFound)
                        {
                            bestDistance = distanceBetweenRooms;
                            possibleConnectionFound = true;
                            bestTileA = tileA;
                            bestTileB = tileB;
                            bestRoomA = roomA;
                            bestRoomB = roomB;
                        }
                    }
                }
            }

            if (possibleConnectionFound && !forceAccessibilityFromMainRoom)
            {
                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            }
        }

        if (possibleConnectionFound && forceAccessibilityFromMainRoom)
        {
            CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
            ConnectClosestRooms(allRooms, true);
        }

        if (!forceAccessibilityFromMainRoom)
        {
            ConnectClosestRooms(allRooms, true);
        }
    }

    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB)
    {
        Room.ConnectRooms(roomA, roomB);

        List<Coord> line = GetLine(tileA, tileB);

        foreach (Coord c in line) // Gets coordinates that is in between rooms
        {
            DrawCircle(c, 3);
        }
    }

    void DrawCircle(Coord c, int r) // Creates a space to expand upon to use in between 2 rooms
    {
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int drawX = c.tileX + x;
                    int drawY = c.tileY + y;
                    if (IsInMapRange(drawX, drawY))
                    {
                        map[drawX, drawY] = 0;
                    }
                }
            }
        }
    }

    List<Coord> GetLine(Coord from, Coord to) // Gets a line between two points closest in between two rooms
    {
        List<Coord> line = new List<Coord>();

        int x = from.tileX;
        int y = from.tileY;

        int dx = to.tileX - from.tileX;
        int dy = to.tileY - from.tileY;

        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);

        int longest = Mathf.Abs(dx);
        int shortest = Mathf.Abs(dy);

        if (longest < shortest)
        {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);

            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }

        int gradientAccumulation = longest / 2;

        for (int i = 0; i < longest; i++)
        {
            line.Add(new Coord(x, y));

            if (inverted)
            {
                y += step;
            }
            else
            {
                x += step;
            }

            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest)
            {
                if (inverted)
                {
                    x += gradientStep;
                }
                else
                {
                    y += gradientStep;
                }
                gradientAccumulation -= longest;
            }
        }

        return line;
    }

    Vector3 CoordToWorldPoint(Coord tile) // Gets a vector conversion from the points
    {
        return new Vector3(-width / 2 + .5f + tile.tileX, 2, -height / 2 + .5f + tile.tileY);
    }

    List<List<Coord>> GetRegions(int tileType) // Find all big groups of points
    {
        List<List<Coord>> regions = new List<List<Coord>>();
        int[,] mapFlags = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                {
                    List<Coord> newRegion = GetRegionTiles(x, y);
                    regions.Add(newRegion);

                    foreach (Coord tile in newRegion)
                    {
                        mapFlags[tile.tileX, tile.tileY] = 1;
                    }
                }
            }
        }

        return regions;
    }

    List<Coord> GetRegionTiles(int startX, int startY) // Gets all tiles in a region
    {
        List<Coord> tiles = new List<Coord>();
        int[,] mapFlags = new int[width, height];
        int tileType = map[startX, startY];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(new Coord(startX, startY));
        mapFlags[startX, startY] = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            tiles.Add(tile);

            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
            {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX))
                    {
                        if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                        {
                            mapFlags[x, y] = 1;
                            queue.Enqueue(new Coord(x, y));
                        }
                    }
                }
            }
        }

        return tiles;
    }

    bool IsInMapRange(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    void RandomFillMap() // Randomly fills maps with 1's and 0's or Walls and floors respectivelly
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode()); // Changes the seed value to hash

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1) // Sets the borders of the map to be walls
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0; // Randomizes the tiles using a seed
                }
            }
        }
    }

    void SmoothMap() // Smooths map by checking the neighbors and changing the state based on the count of neighboring states
    {
        availablePositions.Clear();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles == 0)
                    availablePositions.Add(new Vector3(-width / 2 + .5f + x, 0, -height / 2 + .5f + y));

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    void GetAllAvailableCoordinates() // Adds vector3s to a list for potential object position spawns
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 0)
                {
                    availablePositions.Add(new Vector3(-width / 2 + .5f + x, 0, -height / 2 + .5f + y));
                }
            }
        }
    }

    /*
    void SpawnObjects()
    {
        ClearObjects();
        GameObject enemyContainer = new GameObject("enemyContainer");
        objectClones.Add(enemyContainer);
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 chosenPos = availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
            availablePositions.Remove(chosenPos);
            Vector3 pos = new Vector3(chosenPos.x, 1, chosenPos.z);
            GameObject enemyClone = (GameObject)Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], pos, Quaternion.identity);
            enemyClone.transform.SetParent(enemyContainer.transform);
            objectClones.Add(enemyClone);
        }

        Vector3 pPos = availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
        availablePositions.Remove(pPos);
        Vector3 playerPos = new Vector3(pPos.x, 1, pPos.z);
        GameObject player = (GameObject)Instantiate(playerPrefab, playerPos, Quaternion.identity);
        myCamera.GetComponent<CameraController>().followPlayer();
        objectClones.Add(player);
    }
    */

    /*
    void ClearObjects()
    {
        myCamera.GetComponent<CameraController>().resetFollow();
        foreach (GameObject a in objectClones)
        {
            Destroy(a);
        }
    }
    */

    int GetSurroundingWallCount(int gridX, int gridY) // Counts the surrounding walls
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (IsInMapRange(neighbourX, neighbourY))
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    struct Coord
    {
        public int tileX;
        public int tileY;

        public Coord(int x, int y)
        {
            tileX = x;
            tileY = y;
        }
    }

    class Room : IComparable<Room>
    {
        public List<Coord> tiles;
        public List<Coord> edgeTiles;
        public List<Room> connectedRooms;
        public int roomSize;

        public bool isAccessibleFromMainRoom;
        public bool isMainRoom;

        public Room()
        {
        }

        public Room(List<Coord> roomTiles, int[,] map)
        {
            tiles = roomTiles;
            roomSize = tiles.Count;
            connectedRooms = new List<Room>();

            edgeTiles = new List<Coord>();
            foreach (Coord tile in tiles) // Adds the edges of a room
            {
                for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
                {
                    for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                    {
                        if (x == tile.tileX || y == tile.tileY)
                        {
                            if (map[x, y] == 1)
                            {
                                edgeTiles.Add(tile);

                            }
                        }
                    }
                }
            }
        }

        public void SetAccessibleFromMainRoom()
        {
            if (!isAccessibleFromMainRoom)
            {
                isAccessibleFromMainRoom = true;
                foreach (Room connectedRoom in connectedRooms)
                {
                    connectedRoom.SetAccessibleFromMainRoom();
                }
            }
        }

        public static void ConnectRooms(Room roomA, Room roomB)
        {
            // Updates the rooms if it is accessible from the main room
            if (roomA.isAccessibleFromMainRoom)
            {
                roomB.SetAccessibleFromMainRoom();
            }
            else if (roomB.isAccessibleFromMainRoom)
            {
                roomA.SetAccessibleFromMainRoom();
            }

            roomA.connectedRooms.Add(roomB);
            roomB.connectedRooms.Add(roomA);
        }

        public bool IsConnected(Room otherRoom)
        {
            return connectedRooms.Contains(otherRoom);
        }

        public int CompareTo(Room otherRoom)
        {
            return otherRoom.roomSize.CompareTo(roomSize);
        }
    }

    // DERIABLE ONLY
    // TODO: Find the maps that are near
    void FindOtherMaps()
    {

    }

    // TODO: Connect the maps together

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaveRoom : MonoBehaviour
{

    public List<Vector3> AvailablePositions = new List<Vector3>();
    public Vector3 center;

    int roomSize;

    // ---- Testing Purpose ---- //
    public void CreatePlaceHolder()
    {
        foreach (Vector3 a in AvailablePositions)
        {
            GameObject g = new GameObject("A Node");
            g.transform.position = a;
            //g.AddComponent<TestNode>();
        }
    }

    public void addPosition(Vector3 roomPos)
    {
        AvailablePositions.Add(roomPos);
    }

    public Vector3 findCenter()
    {
        Vector3 midPoint = Vector3.zero;
        int childCount = 0;

        foreach (Vector3 a in AvailablePositions)
        {
            midPoint += a;
            childCount++;
        }

        midPoint /= childCount;

        foreach (Vector3 a in AvailablePositions) // Assign the middle point and remove it from the list
        {
            if (midPoint == a)
            {
                center = a;
                AvailablePositions.Remove(a);
            }
            else
            {
                // Do nothing Duh
            }
        }

        return midPoint;
    }

    public List<Vector3> AllPositions()
    {
        if (AvailablePositions != null)
            return AvailablePositions;
        else
            return null;

    }

    public void SpawnObjectAtRandomPosition(GameObject obj)
    {
        Vector3 randomPos = AvailablePositions[Random.Range(0, AvailablePositions.Count)];
        GameObject enemyClone = Instantiate(obj, randomPos, transform.rotation) as GameObject;
        AvailablePositions.Remove(randomPos);

        if(enemyClone.GetComponent<Enemy>())
          InitEnemy(enemyClone);
    }
    public void SpawnObjectAtRandomPosition(GameObject obj, GameObject obj2)
    {
        Vector3 randomPos = AvailablePositions[Random.Range(0, AvailablePositions.Count)];
        Vector3 randPos2 = new Vector3(randomPos.x, randomPos.y + 5, randomPos.z);
        GameObject enemyClone = Instantiate(obj, randomPos, transform.rotation) as GameObject;
        GameObject itemClone = Instantiate(obj2, randPos2, transform.rotation) as GameObject;
        AvailablePositions.Remove(randomPos);
        


    }
    void InitEnemy(GameObject enemy)
    {
    }
}

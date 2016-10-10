using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaveRoom : MonoBehaviour
{

    public List<Vector3> AvailablePositions = new List<Vector3>();
    public Vector3 center;

    int roomSize;

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
}

using UnityEngine;
using System.Collections.Generic;

public class Circle : Formation
 {
    int angl;
    int numberOfMembers;
    Vector3 center;

    public Circle(int numberOfMem, Vector3 cent, bool isDimension2D, float distanceToCenter, int angle)
    {
        numberOfMembers = numberOfMem;
        center = cent;
        is2D = isDimension2D;
        distanceMultToEach = distanceToCenter;
        angl = angle;
    }

    public override List<Vector3> FormationPosition()
    {
        List<Vector3> pos = new List<Vector3>();
        for(int i = 0; i < numberOfMembers; i++)
        {
            int ang = i * angl;

            Vector3 p = GetCirclePos(center, distanceMultToEach, ang);
        }

        return pos;
    }

    Vector3 GetCirclePos(Vector3 cent, float rad, int a)
    {
        float angle = a;
        Vector3 pos = Vector3.zero;

        if (!is2D)
        {
            pos.x = cent.x + rad * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.y = cent.y;
            pos.z = cent.z + rad * Mathf.Sin(angle * Mathf.Deg2Rad);
        }
        else
        {
            pos.x = cent.x + rad * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.y = cent.y + rad * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.z = cent.z;
        }

        return pos;
    }
}

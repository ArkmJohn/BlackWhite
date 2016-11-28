using UnityEngine;
using System.Collections.Generic;

public class Circle : Formation
 {
    public int angl = 20;
    public int numberOfMembers;
    public Vector3 center;
    public bool LeaderActive;

    public Circle(int numberOfMem, Vector3 cent, bool isDimension2D, float distanceToCenter, int angle)
    {
        numberOfMembers = numberOfMem;
        center = cent;
        is2D = isDimension2D;
        distanceMultToEach = distanceToCenter;
        angl = angle;
    }

    void Update()
    {
        if (LeaderActive)
            center = transform.position;
    }

    public override List<Vector3> FormationPosition()
    {
        List<Vector3> pos = new List<Vector3>();
        
        for (int i = 0; i <= numberOfMembers; i++)
        {
            int ang = i * angl;

            Vector3 p = GetCirclePos(center, distanceMultToEach, ang);
            pos.Add(p);
            //Debug.Log("Created a pos in " + p);
        }

        return pos;
    }

    Vector3 GetCirclePos(Vector3 cent, float rad, int a)
    {
        float angle = a;
        Vector3 pos;
        //angle = Random.Range(0, a);
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

    void OnDrawGizmos()
    {

        foreach (Vector3 a in FormationPosition())
        {
            Gizmos.color = Color.red;;
            Gizmos.DrawCube(a, Vector3.one);
        }
    }
}

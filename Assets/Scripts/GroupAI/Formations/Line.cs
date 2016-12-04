using UnityEngine;
using System.Collections.Generic;

public class Line : Formation {

    public float Length;
    Vector3 myPos;
    public Line(float distanceMultiplier, float l, bool Dimensionis2D, Vector3 myPos)
    {
        distanceMultToEach = distanceMultiplier;
        Length = l;
        is2D = Dimensionis2D;
        this.myPos = myPos;
    }

    public override List<Vector3> FormationPosition()
    {
        List<Vector3> pos = new List<Vector3>();

        for (float x = 0; x < Length; x++)
        {
            if (!is2D)
                pos.Add(new Vector3(myPos.x, 0, x * distanceMultToEach + myPos.z));
            else
                pos.Add(new Vector3(myPos.x, x + myPos.y) * distanceMultToEach);

            Debug.Log("Created a pos");
        }

        return pos;
    }

    void OnDrawGizmos()
    {

        foreach (Vector3 a in FormationPosition())
        {
            Gizmos.DrawCube(a, Vector3.one);
        }
    }
}

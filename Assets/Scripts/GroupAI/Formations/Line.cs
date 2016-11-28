using UnityEngine;
using System.Collections.Generic;

public class Line : Formation {

    public float Length;

    public Line(float distanceMultiplier, float l, bool Dimensionis2D)
    {
        distanceMultToEach = distanceMultiplier;
        Length = l;
        is2D = Dimensionis2D;
    }

    public override List<Vector3> FormationPosition()
    {
        List<Vector3> pos = new List<Vector3>();

        for (float x = 0; x < Length; x++)
        {
            Vector3 inc = new Vector3(transform.forward.x, 0, transform.forward.z);
            if (x == 0)
                pos.Add(transform.position);
            else if (!is2D)
                pos.Add(-transform.forward * distanceMultToEach * x);
            else
                pos.Add(-transform.up * (distanceMultToEach * x));

            //if (!is2D)
            //    pos.Add(new Vector3(transform.position.x + x * (distanceMultToEach), 0, transform.position.z));
            //else
            //    pos.Add(new Vector3(x + transform.position.x, transform.position.y) * distanceMultToEach);

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

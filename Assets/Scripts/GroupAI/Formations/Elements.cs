using UnityEngine;
using System.Collections.Generic;

public class Elements : Formation {

    public float width;
    public float height;

    public Elements(float distanceMultiplier, float x, float y, bool Dimensionis2D)
    {
        distanceMultToEach = distanceMultiplier;
        width = x;
        height = y;
        is2D = Dimensionis2D;
    }

    public override List<Vector3> FormationPosition()
    {
        List<Vector3> pos = new List<Vector3>();
        for (float x = 0; x <= width; x++)
        {
            for (float y = 0; y <= height; y++)
            {
                if (!is2D)
                    pos.Add(new Vector3(x * distanceMultToEach + transform.position.x, 0, y * distanceMultToEach + transform.position.z));
                else
                    pos.Add(new Vector3(x + transform.position.x, y + transform.position.y) * distanceMultToEach);

                Debug.Log("Created a pos");
            }
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

using UnityEngine;
using System.Collections.Generic;

public class Elements : Formation {

    public float width;
    public float height;
    Vector3 myPos;

    public Elements(float distanceMultiplier, float x, float y, bool Dimensionis2D, Vector3 myPos)
    {
        distanceMultToEach = distanceMultiplier;
        width = x;
        height = y;
        is2D = Dimensionis2D;
        this.myPos = myPos;
    }

    public override List<Vector3> FormationPosition()
    {
        List<Vector3> pos = new List<Vector3>();
        //pos.Add(new Vector3(myPos.x, 0, myPos.y));
        for (float x = 0; x <= width; x++)
        {
            for (float y = 0; y <= height; y++)
            {
                if (!is2D)
                    pos.Add(new Vector3(x * distanceMultToEach + myPos.x, 0, y * distanceMultToEach + myPos.z));
                else
                    pos.Add(new Vector3(x + myPos.x, y + myPos.y) * distanceMultToEach);

                //Debug.Log("Created a pos");
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

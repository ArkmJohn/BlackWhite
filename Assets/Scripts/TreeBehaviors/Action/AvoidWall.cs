using UnityEngine;
using System.Collections;
using System;

public class AvoidWall : Node
{
    float maxSeeAhead = 3;
    Vector3 ahead;
    Vector3 targetPosition;
    Vector3 desiredVelocity;
    Enemy me;

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
        me = enemy;
        // Calculate the distance from the obstacle we want to start acting
        ahead = me.transform.position + me.velocity.normalized * maxSeeAhead;

        Vector3 ahead2 = me.transform.position + me.velocity.normalized * maxSeeAhead * 0.5f;

        // Get the closest wall here
        
        //Vector3 avoidanceForce = ahead - 

    }

    private float distance(Enemy me, GameObject obstacle)
    {
        Vector3 a = me.transform.position;
        Vector3 b = obstacle.transform.position;
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }

    bool lineInteresectsCircle(Vector3 ahead, Vector3 ahead2, GameObject obstacle)
    {
        bool a = false;
        bool b = false;
        RaycastHit hit;

        if (Physics.Raycast(me.transform.position, ahead, out hit, Vector3.Distance(me.transform.position, ahead)))
        {
            if (hit.collider != null)
            {
                a = true;
            }
            else
                a = false;
        }

        if (Physics.Raycast(me.transform.position, ahead2, out hit, Vector3.Distance(me.transform.position, ahead2)))
        {
            if (hit.collider != null)
            {
                b = true;
            }
            else
                b = false;
        }
        return a || b;
    }
}

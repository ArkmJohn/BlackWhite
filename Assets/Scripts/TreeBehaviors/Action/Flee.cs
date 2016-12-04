using UnityEngine;
using System.Collections;
using System;

public class Flee : Node {

    Vector3 targetPosition;
    Vector3 desiredVelocity;
    Enemy me;

    public Flee(string myName)
    {
        this.myName = myName;

    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
        // Updates the current required variables
        me = enemy;
        targetPosition = me.target.gameObject.transform.position;

        if (!isNear(enemy))
        {
            SuccessState();
            return;
        }

        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position); // Calculates the wanted rotation and velocity
        me.rotation = GetDir(enemy.gameObject.transform.position);

    }

    Vector3 CalculateDesiredVelocity(Vector3 myPos)
    {
        Vector3 linear = Vector3.zero;
        linear = myPos - targetPosition;
        linear.Normalize();
        linear = linear * me.maxAccel;
        return linear;

    }

    Vector3 GetDir(Vector3 myPos)
    {
        Vector3 targetDir = targetPosition - myPos;
        float stepTimes = me._speed * Time.deltaTime + 2;
        Vector3 myDir = Vector3.RotateTowards(me.gameObject.transform.forward, -targetDir, stepTimes, 0.0f);

        return myDir;
    }

    bool isNear(Enemy me)
    {
        if (Vector3.Distance(targetPosition, me.transform.position) <= 5)
        {
            Debug.Log("Too near to the target");
            return true;
        }
        else
            return false;
    }
}

using UnityEngine;
using System.Collections;
using System;

public class Seek : Node {

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
        targetPosition = me.target.gameObject.transform.position;
        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position);
        me.rotation = GetDir(enemy.gameObject.transform.position);
        SuccessState();
    }

    Vector3 CalculateDesiredVelocity(Vector3 myPos)
    {
        Vector3 linear = Vector3.zero;
        linear = targetPosition - myPos;
        linear.Normalize();
        linear = linear * me.maxAccel;
        return linear;

    }

    Vector3 GetDir(Vector3 myPos)
    {
        Vector3 targetDir = targetPosition - myPos;
        float stepTimes = me._speed * Time.deltaTime + 2;
        Vector3 myDir = Vector3.RotateTowards(me.gameObject.transform.forward, targetDir, stepTimes, 0.0f);

        return myDir;
    }
}

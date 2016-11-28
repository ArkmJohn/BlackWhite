using UnityEngine;
using System.Collections;
using System;

public class Arrive : Node 
{

    float targetRadius;
    float slowRadius;
    float overHead = 6;
    float avoidDistance = 3;
    Vector3 targetPosition;
    Vector3 desiredVelocity;
    Enemy me;

    public Arrive()
    {
        targetRadius = 1f;
        slowRadius = 6;
    }

    public Arrive(float  targetR,float slowR)
    {
        targetRadius = targetR;
        slowRadius = slowR;
    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
		//Debug.Log ("In Arrive script");

        me = enemy;
        targetPosition = me.target.gameObject.transform.position;

        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position);
        me.rotation = GetDir(enemy.gameObject.transform.position);
        SuccessState();
    }

    Vector3 CalculateDesiredVelocity(Vector3 myPos)
    {
        Vector3 linear = Vector3.zero;
        linear = targetPosition - me.gameObject.transform.position;
        float distanceToTarget = linear.magnitude;
        float targetSpeed;

        // checks if near
        if (distanceToTarget < targetRadius)
        {
            return Vector3.zero;
        }

        // Calculates the speed based on where the character is
        if (distanceToTarget > slowRadius)
        {
            targetSpeed = me._speed;
        }
        else
            targetSpeed = me._speed * distanceToTarget / slowRadius;

        Vector3 desiredVel = linear;

        // Calculates the speed that the desired velocity should be stepping
        desiredVel.Normalize();
        desiredVel *= targetSpeed;
        desiredVel -= me.velocity;
        desiredVel /= 0.1f;

        // Make sure that the desired velocity does not exceed the maxximum acceleration
        if(desiredVel.magnitude > me.maxAccel)
        {
            desiredVel.Normalize();
            desiredVel *= me.maxAccel;

        }
        return desiredVel;
    }

    Vector3 GetDir(Vector3 myPos)
    {
        Vector3 targetDir = targetPosition - myPos;
        float stepTimes = me._speed * Time.deltaTime + 2;
        Vector3 myDir = Vector3.RotateTowards(me.gameObject.transform.forward, targetDir, stepTimes, 0.0f);

        return myDir;
    }
}

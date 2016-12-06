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

    public Arrive(string myName)
    {
        targetRadius = 2f;
        slowRadius = 6;
        this.myName = myName;
    }

    public Arrive(float  targetR,float slowR, string myName)
    {
        targetRadius = targetR;
        slowRadius = slowR;
        this.myName = myName;

    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
		//Debug.Log ("In Arrive script");

        me = enemy;
        targetPosition = new Vector3(me.target.gameObject.transform.position.x, me.transform.position.y, me.target.gameObject.transform.position.z);


        if (isNear(enemy))
        {
            SuccessState();
            return;
        }
        else if (enemy.Health <= enemy.MaxHealth / 2)
        {
            FailureState();
            return;
        }
        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position);
        me.rotation = GetDir(enemy.gameObject.transform.position);

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
        //Vector3 newDir = new Vector3(0, myDir.y, 0);
        return myDir;
    }

    bool isNear(Enemy me)
    {
        if (Vector3.Distance(targetPosition, me.transform.position) <= 2)
        {
            Debug.Log("Reached the target");
            return true;
        }
        else
            return false;
    }
}

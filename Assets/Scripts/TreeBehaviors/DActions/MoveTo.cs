using UnityEngine;
using System.Collections.Generic;
using System;

public class MoveTo : Node
{
    Vector3 target;
    GameObject targetObj;
    float isNearRange = 10f;
    float targetRadius;
    float slowRadius;
    float overHead = 6;
    Vector3 targetPosition;
    Vector3 desiredVelocity;
    Enemy me;

    public MoveTo(GameObject targetObject)
    {
        target = targetObject.transform.position;
        targetObj = targetObject;
    }

    public MoveTo(GameObject targetObject, float range)
    {
        target = targetObject.transform.position;
        targetObj = targetObject;

    }
    public MoveTo(GameObject targetObject, float range, float nearFactor)
    {
        target = targetObject.transform.position;
        isNearRange = nearFactor;
        targetObj = targetObject;

    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
        me = enemy;
        targetPosition = findNextPos();

        if (isOnTarget())
            SuccessState();
        else
            FailureState();

        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position);
        me.rotation = GetDir(enemy.gameObject.transform.position);

       
    }

    public bool isOnTarget()
    {

        if (Vector3.Distance(me.gameObject.transform.position, targetPosition) <= isNearRange)
        {
            return true;
        }
        else
            return false;
    }

    public Vector3 findNextPos()
    {
        Vector3 myNextPos = Vector3.zero;

            Stack<GameObject> path = Dikstras.dijkstra(GameObject.FindGameObjectsWithTag("TileAI"), me.NearestNode(), targetObj);
            if (path.Peek() != null)
                myNextPos = path.Peek().gameObject.transform.position;
            else
                myNextPos = me.transform.position;
            Debug.Log(path.Peek().gameObject.name + path.Peek().gameObject.transform.position);

        
        return myNextPos;
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
        if (desiredVel.magnitude > me.maxAccel)
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

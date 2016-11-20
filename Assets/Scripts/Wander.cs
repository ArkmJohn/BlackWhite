using UnityEngine;
using System.Collections.Generic;
using System;

public class Wander : Node
{

    Vector3 targetPosition;
    Vector3 desiredVelocity;
    Enemy me;
	AvoidWallTest wallObj;
    List<GameObject> points;

    float targetRadius = 1;
    float slowRadius = 3;
    float time, myTime;
    bool isUsingPoints = false;

    public Wander(List<GameObject> points, float timer)
    {
        this.points = points;
        isUsingPoints = true;
        time = timer;
    }

    public Wander(float timer)
    {
        isUsingPoints = false;
        time = timer;

    }

    public Wander(List<GameObject> points)
    {
        this.points = points;
        isUsingPoints = true;
        time = 3;
    }

    public Wander()
    {
        isUsingPoints = false;
        time = 0.1f;
    }

    public override void Start()
    {
        currentState = NodeStates.RUNNING;
        myTime = time;
		wallObj = GameObject.Find("enemy").GetComponent<AvoidWallTest>();
    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
//		Debug.Log ("Target Position : " + targetPosition);
        me = enemy;
        if (targetPosition == Vector3.zero)
            targetPosition = findTarget();
        else
        {
            myTime -= Time.deltaTime;
			//Debug.Log ("myTime : " + myTime);
            if (myTime <= 0)
            {
				Debug.Log ("Timer reached 0");
                targetPosition = findTarget();

                myTime = time;
            }
        }

		if(wallObj.avoidWall)
		{
			Debug.Log ("Avoid wall is true. Changing target position");
			targetPosition = findTarget ();
			wallObj.avoidWall = false;
		}


        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position);
		me.angular = GetDir(enemy.gameObject.transform.position);

        SuccessState();
    }

    Vector3 findTarget()
    {
        if (!isUsingPoints)
        {
            float x = UnityEngine.Random.Range(-50, 50);
            float z = UnityEngine.Random.Range(-50, 50);
            return new Vector3(x, 0, z);
        }
        else
        {
            int index = UnityEngine.Random.Range(0, points.Count);
            return points[index].transform.position;
        }


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
		Vector3 myDir = Vector3.RotateTowards(me.gameObject.transform.position, targetDir, stepTimes, 0);

		return targetDir;
    }
}

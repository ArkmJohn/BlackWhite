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

    public Wander(List<GameObject> points , string myName)
    {
        this.points = points;
        isUsingPoints = true;
        time = 2;
        this.myName = myName;
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
		//Debug.Log ("In Wander Script");
    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {

		//Debug.Log ("In Wander Script");
//		Debug.Log ("Target Position : " + targetPosition);
        me = enemy;
        if(wallObj == null)
            wallObj = me.gameObject.GetComponent<AvoidWallTest>();

        if (isNear(enemy))
        {
            targetPosition = findTarget();
            return;
        }
        if (targetPosition == Vector3.zero)
            targetPosition = findTarget();
        //else
        //{
        //    //         myTime -= Time.deltaTime;
        //    ////Debug.Log ("myTime : " + myTime);
        //    //         if (myTime <= 0)
        //    //         {
        //    //	Debug.Log ("Timer reached 0");
        //    //             targetPosition = findTarget();

        //    //             myTime = time;
        //    //         }
        //    //Debug.Log(targetPosition);
        //    float myPos = Vector3.Distance(enemy.gameObject.transform.position, targetPosition);
        //    if (myPos <= 2)
        //    {
        //        targetPosition = findTarget();
        //    }
        //}
        //Debug.Log(targetPosition);
		if(wallObj.avoidWall)
		{
			Debug.Log ("Avoid wall is true. Changing target position");
			targetPosition = findTarget ();
			wallObj.avoidWall = false;
		}


        me.linear = CalculateDesiredVelocity(enemy.gameObject.transform.position);
		me.angular = GetDir(enemy.gameObject.transform.position);
        if (isNear(enemy))
        {
            SuccessState();
        }
        else
            FailureState();
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
            int index = 0; 
            index = UnityEngine.Random.Range(0, points.Count);
            return points[index].transform.position;
            
        }


    }
    bool isNear(Enemy me)
    {
        if (Vector3.Distance(targetPosition, me.transform.position) <= 2)
        {
            Debug.Log("Im Near the target");

            return true;
        }
        else
            return false;
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

﻿using UnityEngine;
using System.Collections;
using System;

public class IsPlayerInRange : Node
{
    GameObject targetObject;
    Vector3 targetPosition;
    Vector3 desiredVelocity;
    Enemy me;

	public override void reset()
	{
		Start();
	}

	public IsPlayerInRange(GameObject target, float range)
    {
        targetObject = target;
    }

    public override void act(Enemy enemy)
    {
        if (isPlayerInRange(targetObject))
            SuccessState();
        else
            FailureState();
    }

    bool isPlayerInRange(GameObject target)
    {
        bool a = false;
        return a;
    }
}

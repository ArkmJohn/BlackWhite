using UnityEngine;
using System.Collections;
using System;

public class IsPlayerInRange : Node
{
    float range;
    Enemy me;

	public override void reset()
	{
		Start();
	}

	public IsPlayerInRange(float range, string myName)
	{
        this.myName = myName;
        this.range = range;
	}

	public override void act(Enemy enemy)
	{
        me = enemy;
        if (me.target != null)
        {
            if (isPlayerInRange(me.target))
                SuccessState();
            else
                FailureState();
        }
        else
            FailureState();
	}

	bool isPlayerInRange(GameObject target)
	{
        if (Vector3.Distance(target.transform.position, me.transform.position) < range)
        {
            return true;
        }
        else
            return false;
	}
}

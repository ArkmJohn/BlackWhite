using UnityEngine;
using System.Collections;
using System;

public class IsInGroup : Node {

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
        if (enemy.myGroup != null)
        {
            SuccessState();
        }
        else
            FailureState();
    }
}

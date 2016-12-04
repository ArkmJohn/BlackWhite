using UnityEngine;
using System.Collections;
using System;

// Start node that repeats after each traversal
public class Repeat : Node {

    private Node node;
    private int times;
    private int originalTimes;

    public Repeat(Node node, string myName)
    {
        this.node = node;
        this.times = -1; // Infinite times
        this.originalTimes = times;
        this.myName = myName;

    }

    public Repeat(Node node, int times, string myName)
    {
        if (times < 1)
        {
            return;
        }

        this.node = node;
        this.times = times;
        this.originalTimes = times;
        this.myName = myName;

    }

    public override void Start()
    {
        this.currentState = NodeStates.RUNNING;
        this.node.Start();
    }

    public override void reset()
    {
        // reset counters
        this.times = originalTimes;
    }

    public override void act(Enemy enemy)
    {
        // Checks if the Node fails
        /*if (node.isFail())
        {
            FailureState();
        }
        else*/ if (node.isSuccess() || node.isFail())
        {

            if (times == 0) // Finish the whole traversal
            {
                SuccessState();
                return;
            }
            if (times > 0 || times <= -1) // Restarts the traversal
            {
                times--;
                node.reset();
                node.Start();
            }

        }
        if (node.isRunning()) // Starts this Node
        {
            node.act(enemy);
        }

    }
}

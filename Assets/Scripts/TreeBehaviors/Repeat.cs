using UnityEngine;
using System.Collections;
using System;

// Start node that repeats after each traversal
public class Repeat : Node {

    private Node node;
    private int times;
    private int originalTimes;

    public Repeat(Node node)
    {
        this.node = node;
        this.times = -1; // Infinite times
        this.originalTimes = times;
    }

    public Repeat(Node node, int times)
    {
        if (times < 1)
        {
            return;
        }

        this.node = node;
        this.times = times;
        this.originalTimes = times;
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
        if (node.isFail())
        {
            FailureState();
        }
        else if (node.isSuccess())
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

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Sequence : Node
{
    public override bool IsSpecial
    {
        get { return true; }
        set { }
    }
    public Sequence()
    {
        this.currentNode = null;
    }

    private Node currentNode;
    List<Node> Nodes = new List<Node>();
    Queue<Node> NodeQueue = new Queue<Node>();

    public void addNode(Node Node)
    {
        Nodes.Add(Node);
    }

    public override void reset()
    {
        foreach (Node Node in Nodes)
        {
            Node.reset();
        }
    }

    public override void Start()
    {
        // starts the current sequence
        this.currentState = NodeStates.RUNNING;
        
        // reset the current queue
        NodeQueue.Clear();
        foreach (Node Node in Nodes)
        {
            NodeQueue.Enqueue(Node);

            // To Queue all Nodes in special Nodes before the game starts
            if (Node.IsSpecial)
            {
                Node.Start();
            }
        }
        currentNode = NodeQueue.Dequeue();
        if (!currentNode.isRunning())
            currentNode.Start();
    }

    public override void act(Enemy enemy)
    {
        currentNode.act(enemy);
        // if is still running then it will carry on
        if (currentNode.isRunning())
        {
            return;
        }

        if (currentNode != null && NodeQueue.Count != 0)
        {
            if (currentNode.isFail())
            {
				FailureState ();
            }
            else if (currentNode.isSuccess())
            {
                currentNode = NodeQueue.Dequeue();
            }
            else
                return;
        }

        if (currentNode.isFail() && NodeQueue.Count == 0)
        {
            FailureState();
        }
        if(currentNode.isSuccess() && NodeQueue.Count == 0)
        {
            SuccessState();
        }
    }
}

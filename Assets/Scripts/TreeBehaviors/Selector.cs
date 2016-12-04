using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class Selector : Node
{
    public override bool IsSpecial
    {
        get { return true; }
        set { }
    }
    public Selector(string myName)
    {
		this.currentNode = null;
        this.myName = myName;

    }

    public Node currentNode;
	public List<Node> nodeList = new List<Node>();
	Queue<Node> nodeQueue = new Queue<Node>();

	public void addNode(Node node)
    {
        nodeList.Add(node);
    }

    public override void reset()
    {
		foreach (Node node in nodeList)
        {
			node.reset();
        }
    }
    public override void Start()
    {
		this.currentState = NodeStates.RUNNING;
        nodeQueue.Clear();

        // Moves the list to the queue and resets the nerve from setup
		foreach (Node node in nodeList)
        {
			nodeQueue.Enqueue(node);
			if (node.IsSpecial)
            {
				node.Start();
            }
        }
		currentNode = nodeQueue.Dequeue();
		if (!currentNode.isRunning ())
			currentNode.Start (); 
    }

	public override void act(Enemy enemy)
    {
		currentNode.act(enemy);

        // Carries on if still running
		if (currentNode.isRunning())
        {
            return;
        }

		if (currentNode != null && nodeQueue.Count != 0)
        {
			if (currentNode.isFail())
            {
				currentNode = nodeQueue.Dequeue();
            }
			else if (currentNode.isSuccess())
            {
				SuccessState();
            }
        }

		if (currentNode.isFail() && nodeQueue.Count == 0)
        {
			FailureState();
        }
		if (currentNode.isSuccess() && nodeQueue.Count == 0)
        {
			SuccessState();
        }

    }
}


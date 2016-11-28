using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Inverter : Node
{
	private Node node;
	List<Node> NodeList = new List<Node>();
	private Node currentNode;

	public override void reset()
	{
		Start ();
	}

	public Inverter()
	{
		this.currentNode = null;
	}


	public void addNode(Node Node)
	{
		NodeList.Add(Node);
	}

	public Inverter(Node node)
	{
		this.node = node;
	}

	public override void act(Enemy enemy)
	{
		NodeList[0].act (enemy);
		if (node.isFail () && node != null) {
			this.SuccessState ();
		} else if (node.isSuccess () != null) {
			this.FailureState ();
		} else
			this.FailureState ();
	}
}

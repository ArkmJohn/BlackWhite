using UnityEngine;
using System.Collections.Generic;
using System;

public class TestTree : Tree
{
	public Enemy me;
	Repeat brain;
	public Selector selector1, selector2;
	public Sequence sequence1,sequence2, sequence3;
	public Inverter inverter1;
    public float Range = 5;

	GameObject enemyTarget;

	void Awake()
	{
		InitTree();
	}

	public override void InitTree()
	{
		me = GetComponent<Enemy>();
		enemyTarget = me.target;
		BuildTree();

		me.routine = brain;
		me.Init();
	}

	public override void AIAct()
	{

	}

	void Update()
	{
        if(!me.isAttacking)
		    me.Tick();
	}

	public override void BuildTree()
	{
		selector1 = new Selector ("selector1");
		selector2 = new Selector ("selector2");
		sequence1 = new Sequence ("sequence1");
		sequence2 = new Sequence ("sequence2");
		sequence3 = new Sequence ("sequence3");
		inverter1 = new Inverter ();

		//inverter1.addNode (a);z
		sequence3.addNode (new Arrive ("Going SomeWhere"));
		sequence3.addNode (new Attack ("Attacking Something"));
        //sequence3.addNode (inverter1);
        DebugNodes(sequence3, sequence3.NodeList);
        Debug.Log("Created Sequence 3 with " + sequence3.NodeList.Count + " Nodes");

        sequence2.addNode(new IsHealthLow("IsHealthLow?"));
        sequence2.addNode(new Flee("Fleeing"));
        DebugNodes(sequence2, sequence2.NodeList);
        Debug.Log("Created Sequence 2 with " + sequence2.NodeList.Count + " Nodes");

        //IsHealthLow a = new IsHealthLow ("IsHealthLow?");
        selector2.addNode(sequence2);
        selector2.addNode(sequence3);
        DebugNodes(selector2, selector2.nodeList);
        Debug.Log("Created Selector 2 with " + selector2.nodeList.Count + " Nodes");

        sequence1.addNode(new IsPlayerInRange(Range, "Can I See The Player"));
        sequence1.addNode(selector2);
        DebugNodes(sequence1, sequence1.NodeList);
        Debug.Log("Created Sequence 1 with " + sequence1.NodeList.Count + " Nodes");

        selector1.addNode(sequence1);
		//selector1.addNode (new Wander ());
		// TODO : Uncomment for the main level scene.
        selector1.addNode(new Wander(FindObjectOfType<LevelGenerator>().aTilePos, "Wander"));
        DebugNodes(selector1, selector1.nodeList);
        Debug.Log("Created Selector 1 with " + selector1.nodeList.Count + " Nodes");

        brain = new Repeat(selector1, "Repeating");
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);

    }

    void DebugNodes(Node parentNode,List<Node> nodes)
    {
        foreach (Node a in nodes)
        {
            //Debug.Log("Added " + a.myName + " to " + parentNode.myName);
        }
    }

}

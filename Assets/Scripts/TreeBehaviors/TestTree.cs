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
		me.Tick();
	}

	public override void BuildTree()
	{
		selector1 = new Selector ();
		selector2 = new Selector ();
		sequence1 = new Sequence ();
		sequence2 = new Sequence ();
		sequence3 = new Sequence ();
		inverter1 = new Inverter ();

		/*inverter1.addNode (new IsHealthLow());
		sequence1.addNode (new Arrive ());
		sequence1.addNode (new Attack ());
		sequence1.addNode (inverter1);


		sequence2.addNode (new IsHealthLow ());
		sequence2.addNode (new Flee ());

		selector1.addNode (sequence2);
		selector1.addNode (sequence1);

		sequence3.addNode (new IsPlayerInRange (enemyTarget, 15f));
		sequence3.addNode (selector1);

		selector2.addNode (sequence3);
		selector2.addNode (new Wander ());*/

		selector1.addNode (sequence1);
		selector1.addNode (new Wander ());

		sequence1.addNode(new IsPlayerInRange (enemyTarget, 15f));
		sequence1.addNode (selector2);

		selector2.addNode (sequence2);
		selector2.addNode (sequence3);

		sequence2.addNode (new IsHealthLow ());
		sequence2.addNode (new Flee ());

		IsHealthLow a = new IsHealthLow ();

		inverter1.addNode (a);
		sequence3.addNode (new Arrive ());
		sequence3.addNode (new Attack ());
		sequence3.addNode (inverter1);


		// TODO : To uncomment once tree is tested
		//selector2.addNode (new Wander (FindObjectOfType<LevelGenerator>().aTilePos));

		brain = new Repeat(selector2);
	}


}

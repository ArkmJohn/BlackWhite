using UnityEngine;
using System.Collections.Generic;
using System;

public class TestTree : Tree
{
    public Enemy me;
    Repeat brain;
    public Selector selector1;
    public Sequence sequence1;

    void Awake()
    {
        InitTree();
    }

    public override void InitTree()
    {
        me = GetComponent<Enemy>();
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
        selector1 = new Selector();
        sequence1 = new Sequence();

        sequence1.addNode(new TestNode("I really Hate it but"));
        sequence1.addNode(new TestNode(" , its to late to change"));
        //selector1.addNode(new Seek());
        selector1.addNode(new Wander());
        brain = new Repeat(selector1);
    }

}

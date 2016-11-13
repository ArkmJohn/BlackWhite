using UnityEngine;
using System.Collections.Generic;
using System;

public class TestTree : Tree
{
    public Enemy me;
    Repeat brain;
    public Selector selector1;
    public Sequence sequence1;
    public GameObject target;

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

        //sequence1.addNode(new TestNodeA("I really Hate it but"));
        //sequence1.addNode(new TestNodeA(" , its to late to change"));
        sequence1.addNode(new MoveTo(target));
        sequence1.addNode(new TestNodeA("-.- success"));
        //selector1.addNode(new Seek());
        //selector1.addNode(new MoveTo(target));
        selector1.addNode(sequence1);
        brain = new Repeat(selector1);
    }

}

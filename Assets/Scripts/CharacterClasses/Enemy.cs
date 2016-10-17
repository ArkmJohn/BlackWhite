using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    public Vector3 targetLocation;
    public GameObject weapon;
    public Tree myAITree;

    public void Update()
    {
        Act();
    }

    // This is where the enemy should put all its ai
    private void Act()
    {
        myAITree.AIAct();
        // Do some other stuff
    }

    // TODO: Set animations and stuff
    public void AttackAnim()
    { }
    public void WalkAnim()
    { }
    public void StandAnim()
    { }
    public void DieAnim()
    { }
}

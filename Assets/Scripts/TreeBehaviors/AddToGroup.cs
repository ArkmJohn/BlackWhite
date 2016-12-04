using UnityEngine;
using System.Collections.Generic;
using System;

public class AddToGroup : Node {

    Enemy me;
    float radius;
    public override void reset()
    {
        Start();
    }

    public override void act(Enemy enemy)
    {
        me = enemy;
        
        float initialMembers = me.GetComponent<Group>().members.Count;

        GetPotentialMembers();

        if (me.GetComponent<Group>().members.Count != initialMembers)
        {
            SuccessState();
        }
        else
        {
            FailureState();
        }
    }

    void GetPotentialMembers()
    {
        List<Enemy> potentialMembers = new List<Enemy>();
        List<Enemy> enemies = GetEnemies();

        foreach (Enemy e in enemies)
        {
            if (IsNear(me.transform.position, e.transform.position))
            {
                potentialMembers.Add(e);
            }
        }

        //me.GetComponent<Group>().Members(potentialMembers);

    }

    bool IsNear(Vector3 me, Vector3 target)
    {
        if (Vector3.Distance(me, target) < radius)
        {
            return true;
        }
        else
            return false;
    }

    List<Enemy> GetEnemies()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        List<Enemy> a = new List<Enemy>();
        foreach (Enemy e in enemies)
        {
            a.Add(e);
        }

        return a;

    }

    
}

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Flock : MonoBehaviour {

    float neighbourDistance = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ApplyFlock(Group myGroup, Enemy e, Vector3 goal)
    {
        // List of Members
        List<Enemy> coMemScr = new List<Enemy>();
        coMemScr = myGroup.members;
        List<GameObject> coMembers = new List<GameObject>();
        for (int x = 0; x < coMemScr.Count; x++)
        {
            coMembers[x] = coMemScr[x].gameObject;
        }

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPos = goal;

        float distance;

        int groupSize = 0;
        foreach (GameObject go in coMembers)
        {
            if (go != this.gameObject)
            {
                // Checks distance and adds the distance of the go
                distance = Vector3.Distance(go.transform.position, this.transform.position);
                if (distance <= neighbourDistance)
                {
                    vCentre += go.transform.position;
                    groupSize++;

                    // Adds up the avoiding of other members
                    if (distance < 1.0f)
                    {
                        vAvoid += (this.transform.position - go.transform.position);
                    }
                    
                }
            }
        }

        if (groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - this.transform.position);

            Vector3 direction = (vCentre + vAvoid) - transform.position;

            if (direction != Vector3.zero)
            {
                //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation)
            }
        }


    }

    public void FlyToCOM(List<GameObject> coMem)
    {
        Vector3 CoM = Vector3.zero;
        foreach (GameObject c in coMem)
        {
            CoM += c.transform.position;

        }
    }
}

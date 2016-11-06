using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    Vector3 followLeader(GameObject leader, List<GameObject> GroupMembers)
    {
        Vector3 leaderPos = leader.transform.position;
        Vector3 velocityAdd;

        // Calculates the position behind the leader
        leaderPos = GetBehindPosition(leaderPos, leader.transform);
        leaderPos.Normalize();
        leaderPos = GetBehindPosition(leaderPos, leader.transform);

        // Creates the vector behind the leader
        velocityAdd = leaderPos;

        // Adds the separation
        velocityAdd += Separation(GroupMembers);

        return velocityAdd;
    }

    Vector3 Separation(List<GameObject> neighbours)
    {
        Vector3 vAvoid = Vector3.zero;
        float distance = 0;
        int neighbouringCount = 0;

        foreach (GameObject a in neighbours)
        {
            if (a != this.gameObject)
            {
                distance = Vector3.Distance(a.transform.position, this.transform.position);
                if (distance <= 1.5f)
                {
                    vAvoid += (this.transform.position - a.transform.position);
                    neighbouringCount++;
                }
            }
        }

        if (neighbouringCount != 0)
        {
            vAvoid /= neighbouringCount;
        }

        vAvoid.Normalize();

        return vAvoid;
    }

    Vector3 GetBehindPosition(Vector3 v, Transform t)
    {
        return v - t.forward;
    }
}

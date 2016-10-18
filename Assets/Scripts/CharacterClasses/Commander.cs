using UnityEngine;
using System.Collections;

public class Commander : Enemy {

    Group myGroup;

	// Use this for initialization
	void Start ()
    {
        myGroup = GetComponent<Group>();
        myGroup.InitGroup(this.GetComponent<Character>());
	}
	
	// Update is called once per frame
	void Update () {
        Act();
	}
}

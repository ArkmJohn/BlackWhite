using UnityEngine;
using System.Collections;

public class Unparent : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

        transform.DetachChildren();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

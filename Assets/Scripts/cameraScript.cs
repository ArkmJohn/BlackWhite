using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

    public GameObject player;
    
    public float xDist = 10f;
    public float yDist = 10f;
    public float zDist = 10f;

    private Vector3 camDistance;
    //player will be attached to this
    void Start () {
        if(player == null)
        {
            //player = FindObjectOfType<Player>();
            player = GameObject.FindGameObjectWithTag("Player");

            camDistance = new Vector3(xDist, yDist, zDist);
        }
        
	
	}
	
	void Update () {
        transform.position = player.transform.position + camDistance;
        transform.LookAt(player.transform.forward);
        //transform.rotation = player.transform.rotation;
	}
}

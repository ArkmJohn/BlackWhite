using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

    public GameObject player;
    
    public float xDist = 0;
    public float yDist = 0;
    public float zDist = 0;

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
        //transform.position = player.transform.position + camDistance;
        transform.LookAt(player.transform);
        //transform.Rotate(xAngle) = player.transform.rotation.y;
	}
}

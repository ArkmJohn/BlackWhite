using UnityEngine;
using System.Collections;

public class ArrowPhysics : MonoBehaviour {
    public Vector3 velocity;
    //public Quaternion myRotation = Quaternion.Euler(0, 0, 0);
    private Rigidbody rb;
    private float myRotation = 5f;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * 500f);
        //  velocity = rb.velocity;

    }
	void Update () {

       // rb.velocity = velocity;
       // gameObject.transform.Rotate(myRotation * Time.deltaTime, 0, 0);
    }
}

using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    [SerializeField]
    private float speed = 100;
    bool moving;
    public bool isPLayCam;

    public void ChangeCameraToThisPos()
    {
        GameObject a = GameObject.FindGameObjectWithTag("MainCamera");
        if (!isPLayCam)
            moving = true;
        else
            SetCamPos();
    }

    void Update()
    {
        if (moving == true)
        {
            MoveCamera();
            if (GameObject.FindGameObjectWithTag("MainCamera").transform.position == this.transform.position && GameObject.FindGameObjectWithTag("MainCamera").transform.rotation == this.transform.rotation)
                moving = false;
        }
    }

    void MoveCamera()
    {
        GameObject a = GameObject.FindGameObjectWithTag("MainCamera");
        //a.gameObject.transform.position = this.transform.position;
        a.transform.position = Vector3.MoveTowards(a.transform.position, this.transform.position, Time.deltaTime * speed);
        // a.transform.rotation = this.transform.rotation;
        a.transform.rotation = Quaternion.RotateTowards(a.transform.rotation, this.transform.rotation, Time.deltaTime * speed * 5);
    }

    void SetCamPos()
    {
        GameObject a = GameObject.FindGameObjectWithTag("MainCamera");
        a.gameObject.transform.position = this.transform.position;
        a.transform.rotation = this.transform.rotation;
    }
}

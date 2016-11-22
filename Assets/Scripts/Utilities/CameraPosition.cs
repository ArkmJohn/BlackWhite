using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    public bool isPlayCam = false, isMoving = false;
    public float speed = 30;

    public void ChangeCameraToThisPos()
    {
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            if (!isPlayCam)
                MoveCamera();
            else
                SetCamera();

            if (IsInPosition())
                isMoving = false;
        }
    }

    void SetCamera()
    {
        GameObject a = GameObject.FindGameObjectWithTag("MainCamera");

        a.transform.position = this.transform.position;
        a.transform.rotation = this.transform.rotation;
    }

    void MoveCamera()
    {
        GameObject a = GameObject.FindGameObjectWithTag("MainCamera");

        a.transform.position = Vector3.MoveTowards(a.transform.position, this.transform.position, speed * Time.deltaTime);
        a.transform.rotation = Quaternion.RotateTowards (a.transform.rotation, this.transform.rotation, speed * 5 * Time.deltaTime);
    }

    bool IsInPosition()
    {
        GameObject a = GameObject.FindGameObjectWithTag("MainCamera");
        if (a.transform.position == this.transform.position && a.transform.rotation == this.transform.rotation)
            return true;
        else
            return false;

    }
}

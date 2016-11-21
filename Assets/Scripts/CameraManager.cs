using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public GameObject playCamPos, pauseCamPos, loseCamPos, winCamPos, myCamera;

    public void switchCamPos(int index)
    {
        switch (index)
        {
            case 1: // play
                playCamPos.GetComponent<CameraPosition>().ChangeCameraToThisPos();
                break;
            case 2: // pause
                pauseCamPos.GetComponent<CameraPosition>().ChangeCameraToThisPos();
                break;
            case 3: // lose
                loseCamPos.GetComponent<CameraPosition>().ChangeCameraToThisPos();
                break;
            case 4: // win
                winCamPos.GetComponent<CameraPosition>().ChangeCameraToThisPos();
                break;
        }
    }
}

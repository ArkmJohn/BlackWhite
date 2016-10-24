using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {

    private CanvasScaler canvasScaler;

    // Use this for initialization
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();

        //change UI scale mode to 'scale with screen size' after game started
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

    // Update is called once per frame
    void Update () {
	
	}
}

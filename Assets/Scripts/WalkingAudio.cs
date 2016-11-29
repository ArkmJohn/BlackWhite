using UnityEngine;
using System.Collections;

public class WalkingAudio : MonoBehaviour {

    public AudioSource walkingsound;
    public bool playsound = false;

    public void WalkingAudioScr()
    {
        if (playsound == false) {
            walkingsound.Play();
        }
        
    }

    public void WalkingAudioScrStop()
    {
        if (playsound == true) {
            walkingsound.Stop();
        }
    }

    
}

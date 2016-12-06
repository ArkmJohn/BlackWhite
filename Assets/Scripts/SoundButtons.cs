using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class SoundButtons : MonoBehaviour {

    public AudioMixer myMixer;
    private bool backgroundBool = true;
    private bool effectsBool = true;

    public void soundButton()
    {
        if (effectsBool)
        {
            myMixer.SetFloat("soundeffectS", -80f);
            effectsBool = false;
        }
        else
        {
            myMixer.SetFloat("soundeffectS", 0f);
            effectsBool = true;
        }
        Debug.Log("Effects Pressed");
    }

    public void musicButton()
    {
        if (backgroundBool)
        {
            myMixer.SetFloat("backgroundS", -80f);
            backgroundBool = false;
        }
        else
        {
            myMixer.SetFloat("backgroundS", 0f);
            backgroundBool = true;
        }
        Debug.Log("background Pressed");
    }

}

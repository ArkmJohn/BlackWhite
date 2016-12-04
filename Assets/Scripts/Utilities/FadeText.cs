using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour {

    public bool isActive;
    public float maxFadeDuration = 2, fadeDuration;


    void OnEnable()
    {
        isActive = true;
        fadeDuration = maxFadeDuration; // Resets the timer
        
    }

    void Update()
    {
        if (isActive)
        {
            fadeDuration -= Time.deltaTime;
            Color color = GetComponent<Text>().color;
            float myRatio = Time.deltaTime / fadeDuration;
            color.a = Mathf.Lerp(1, 0, myRatio);
            GetComponent<Text>().color = color;

            if (fadeDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

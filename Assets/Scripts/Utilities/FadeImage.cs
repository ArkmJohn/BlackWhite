using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{

    public bool isActive;
    public float maxFadeDuration = 2, fadeDuration;


    void OnEnable()
    {
        isActive = true;
        fadeDuration = maxFadeDuration; // Resets the timer
        print("script was enabled");
    }

    void Update()
    {
        if (isActive)
        {
            fadeDuration -= Time.deltaTime;
            Color color = GetComponent<Image>().color;
            float myRatio = Time.deltaTime / fadeDuration;
            color.a = Mathf.Lerp(1, 0, myRatio);
            GetComponent<Image>().color = color;

            if (fadeDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

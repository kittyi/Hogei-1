using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour
{

    [Header("Timing vars")]
    [Tooltip("Speed of one fade transition")]
    public float fadeSpeed = 0.5f;

    [Header("Image ref")]
    public Image imageRef;

    //control vars
    private bool isFadeIn = false;
    private bool isFadeOut = false;
    private float alphaLevel = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FadeIn();
        FadeOut();
    }

    //fade image alpha to black
    private void FadeIn()
    {
        if (isFadeIn)
        {
            //change alpha relative to time
            alphaLevel += fadeSpeed * Time.deltaTime;
            //change image colour
            imageRef.color = new Color(0, 0, 0, alphaLevel);
            //keep values between 0 and 1
            alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
            //if done, stop
            if (alphaLevel == 1)
            {
                isFadeIn = false;
            }
        }
    }

    //fade image alpha to transparent
    private void FadeOut()
    {
        if (isFadeOut)
        {
            //change alpha relative to time
            alphaLevel -= fadeSpeed * Time.deltaTime;
            //change image colour
            imageRef.color = new Color(0, 0, 0, alphaLevel);
            //keep values between 0 and 1
            alphaLevel = Mathf.Clamp(alphaLevel, 0.0f, 1.0f);
            //if done, stop
            if (alphaLevel == 0)
            {
                isFadeOut = false;
            }
        }
    }

    //funcs to start fading funcs
    public void StartFadeIn()
    {
        isFadeIn = true;
    }

    public void StartFadeOut()
    {
        isFadeOut = true;
    }
}

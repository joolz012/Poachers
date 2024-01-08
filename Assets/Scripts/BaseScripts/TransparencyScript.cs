using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyScript : MonoBehaviour
{
    public float fadeSpeed;
    public float fadeAmount;
    private float originalOpacity;
    public bool doFade;
    Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalOpacity = mat.color.a;
    }

    private void Update()
    {
        if(doFade)
        {
            Fade();
        }
        else
        {
            ResetFade();
        }
    }

    void Fade()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed));
        mat.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = mat.color;
        Color smoothColor = new(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed));
        mat.color = smoothColor;
    }
}

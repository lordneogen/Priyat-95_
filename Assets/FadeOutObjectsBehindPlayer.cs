using System;
using DG.Tweening;
using UnityEngine;

public class FadeOutObjectsBehindPlayer : MonoBehaviour
{
    public float fadeSpeed, fadeAmount;
    private float originalOpacity;
    private Material[] Mats;
    public bool DoFade;

    private void Start()
    {
        Mats = GetComponent<Renderer>().materials;
        foreach (var x in Mats)
        {
            originalOpacity = x.color.a;
        }
    }

    private void Update()
    {
        if(DoFade) FadeNow();
        else ResetFade();
    }

    private void FadeNow()
    {
        foreach (var x in Mats)
        {
            Color Curcolor = x.color;
            Color Smothcolor = Curcolor;
            Smothcolor.a = Mathf.Lerp(Curcolor.a, fadeAmount, fadeSpeed * Time.deltaTime);
            x.color=Smothcolor;
        }
    }
    
    private void ResetFade()
    {
        foreach (var x in Mats)
        {
            Color Curcolor = x.color;
            Color Smothcolor = Curcolor;
            Smothcolor.a = Mathf.Lerp(Curcolor.a, originalOpacity, fadeSpeed * Time.deltaTime);
            x.color=Smothcolor;
        }
    }
}
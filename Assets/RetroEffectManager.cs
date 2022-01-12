using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RetroEffectManager : MonoBehaviour
{

    public bool stopEffects;
    public RawImage renderImg;
    private Material renderImgMat;

    public static RetroEffectManager instance;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        renderImgMat = renderImg.material;
    }
    // Start is called before the first frame updat

    public void StartStopAllRetroEffects()
    {
        stopEffects = !stopEffects;
        if (stopEffects)
        {
            renderImg.material = null;
            Camera.main.GetComponent<Volume>().enabled = false;
        }
        else
        {
            Camera.main.GetComponent<Volume>().enabled = true;
            renderImg.material = renderImgMat;
        }
    }
}

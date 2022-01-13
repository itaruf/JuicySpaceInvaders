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
        cameraGlitch.SetFloat("StaticAmount", 0f);
    }
    // Start is called before the first frame updat

    private void Update()
    {
        cameraGlitch.SetFloat("UnscaledTime", Time.unscaledTime);
    }

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


    public void StopTimeEffect()
    {
    StartCoroutine(StopTime());
    }

    public GameObject thunder;
    public Material cameraGlitch;
    IEnumerator StopTime()
    {
        Time.timeScale = 0;
        cameraGlitch.SetFloat("StaticAmount", 0.5f);
        FindObjectOfType<ThunderManager>().ForceThunder();
        yield return new WaitForSecondsRealtime(2f);
        thunder.SetActive(false);
        cameraGlitch.SetFloat("StaticAmount", 0f);
        Time.timeScale = 1;
    }


}

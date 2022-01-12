using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public enum ShakeType
    {
        Horizontal,
        Vertical,
        Both
    };

    //private float currentShakeMagnitude = 0f;
    //private bool isShaking = false;
    private float xOffset;
    private float yOffset;
    Vector3 originalPos;
    public bool stopShake = false;

    private static CameraShake _instance;
    public static CameraShake Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

void Start()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;

        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (stopShake)
            StopAllCoroutines();
    }

    public IEnumerator ShakeCamera(float duration, float magnitude, float minRandomOffset, float maxRandomOffset, ShakeType shakeType) 
    {
        //currentShakeMagnitude = magnitude;

        float elapsedTime = 0f;

        switch (shakeType)
        {
            case ShakeType.Horizontal:
                {
                    while (elapsedTime < duration)
                    {
                        this.xOffset = Random.Range(minRandomOffset, maxRandomOffset) * magnitude / 100;
                        transform.localPosition = new Vector3(this.xOffset, originalPos.y, originalPos.z);

                        elapsedTime += Time.deltaTime;

                        yield return 0;
                    }
                }
                break;
            case ShakeType.Vertical:
                {
                    while (elapsedTime < duration)
                    {
                        this.yOffset = Random.Range(minRandomOffset, maxRandomOffset) * magnitude / 100;
                        transform.localPosition = new Vector3(originalPos.x, this.yOffset, originalPos.z);

                        elapsedTime += Time.deltaTime;

                        yield return 0;
                    }
                }
                break;
            case ShakeType.Both:
                {
                    while (elapsedTime < duration)
                    {
                        this.xOffset = Random.Range(minRandomOffset, maxRandomOffset) * magnitude / 100;
                        this.yOffset = Random.Range(minRandomOffset, maxRandomOffset) * magnitude / 100;
                        transform.localPosition = new Vector3(this.xOffset, this.yOffset, originalPos.z);

                        elapsedTime += Time.deltaTime;

                        yield return 0;
                    }

                }
                break;
            default:
                break;
        }

        transform.localPosition = originalPos;
        this.xOffset = originalPos.x;
        this.yOffset = originalPos.y;
        //currentShakeMagnitude = 0f;
    }

    public void OnStartShakeCamera(float duration, float magnitude, float minRandomOffset, float maxRandomOffset, ShakeType shakeType)
    {
        if (!stopShake)
            StartCoroutine(ShakeCamera(duration, magnitude, minRandomOffset, maxRandomOffset, shakeType));
    }
}


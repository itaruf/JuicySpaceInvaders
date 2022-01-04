using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] public float duration;
    [SerializeField] public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude / 100;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude / 100;

            transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return 0;
        }
        transform.localPosition = originalPos;
    }
}

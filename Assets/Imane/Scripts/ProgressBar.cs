using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public Score score;
    void Start()
    {
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetProgressBar(10);
            score.SetScore(10);
        }
    }

    public void SetProgressBar(float value)
    {
        slider.value += value / 100;
    }
}

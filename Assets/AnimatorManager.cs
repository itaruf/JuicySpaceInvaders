using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public bool isAnimating = true;

    private static AnimatorManager _instance;
    public static AnimatorManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }


    public void StartStopAnim()
    {
        isAnimating = !isAnimating;
    }

}


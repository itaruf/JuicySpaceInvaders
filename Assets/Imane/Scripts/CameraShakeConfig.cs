using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraShakeConfig
{
    public float duration;
    public float magnitude;
    public float minRange;
    public float maxRange;
    public CameraShake.ShakeType shakeType;
}

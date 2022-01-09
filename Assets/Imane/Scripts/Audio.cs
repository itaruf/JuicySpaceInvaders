using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Collections;

[Serializable]
public class Audio
{
    [Serializable]
    public enum AudioType
    {
        BACKGROUND,
        SFX,
    };

    [SerializeField] public AudioClip clip;
    //[SerializeField] public string name;
    [SerializeField] [Range(0f, 1f)] public float volume;
    [SerializeField] [Range(0f, 3f)] public float pitch;
    [SerializeField] public bool loop;
    [SerializeField] [HideInInspector] public AudioSource source;
    [SerializeField] public AudioType audioType;

    void Start()
    {
        
    }

    void Update()
    {

    }

}

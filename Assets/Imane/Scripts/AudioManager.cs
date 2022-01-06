using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }
    public GameObject audiosParent;
    public GameObject audioModel;
    public List<GameObject> listOfAudios;
    public bool stopAllAudios = false;

    void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(audiosParent);
        }

        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < listOfAudios.Count; i++)
            if (listOfAudios[i] == null)
            {
                listOfAudios.RemoveAt(i);
                i--;
            }
    }

    /******************Pour les GameObjects ayant des audiosources*******************/
    public void PlayAudio(string name, AudioSource[] audioSources, bool resettable)
    {

        AudioSource audioSource = Array.Find(audioSources, sound => sound.clip.name == name);

        // On rejoue l'audio même s'il était déjà en cours ou si aucun n'était en cours
        if (resettable || !audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopAudio(string name, AudioSource[] audioSources)
    {
        AudioSource audio = Array.Find(audioSources, sound => sound.clip.name == name);

        if (audio.isPlaying)
            audio.Stop();
    }

    public void StopAllAudios(AudioSource[] audioSources)
    {
        foreach(AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    /***************Pour les audiosources sur l'AudioManager******************/

    // Pour les audios qu'on veut redémarrer à 0 si on essaie de rejouer le même clip
    public void PlayAudio(string name, Audio.AudioType audioType, bool resettable)
    {
        Audio audio = Array.Find(audios, sound => sound.clip.name == name);
        switch (audioType)
        {
            case Audio.AudioType.BACKGROUND:
                StopAllAudiosByAudioType(audioType);
                audio.source.Play();
                break;
            case Audio.AudioType.SFX:
                if (!resettable)
                {
                    PlayAudio(name, audioType);
                    break;
                }
                StopAllAudiosByAudioType(audioType);
                audio.source.Play();
                break;
            default:
                break;
        }
    }

    // Pour les audios qu'on ne veut pas redémarrer à 0
    public void PlayAudio(string name, Audio.AudioType audioType)
    {
        Audio audio = Array.Find(audios, sound => sound.clip.name == name);

        GameObject audioGameObject = Instantiate(audioModel);
        audioGameObject.transform.parent = audiosParent.transform;

        AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();
        audioSource.clip = audio.clip;
        audioSource.volume = audio.volume;
        audioSource.pitch = audio.pitch;
        audioSource.loop = audio.loop;
        audioSource.Play();

        listOfAudios.Add(audioGameObject);

        if (!audio.source.loop)
            Destroy(audioGameObject, audioSource.clip.length);
    }

    // Stop un audio en particulier d'un certain type
    public void StopAudio(string name, Audio.AudioType audioType, bool resettable = true)
    {
        Audio audio = Array.Find(audios, sound => sound.clip.name == name);

        if (audio.audioType == audioType)
            audio.source.Stop();
    }

    // Stop absolument tous les audios en cours
    public IEnumerator StopAllAudios()
    {
        stopAllAudios = true;

        foreach (Audio audio in audios)
            if (audio.source.isPlaying)
                audio.source.Stop();

        foreach (GameObject audio in listOfAudios)
            Destroy(audio.gameObject);

        yield return new WaitForEndOfFrame();

        stopAllAudios = false;
    }

    // Stop absolument tous les audios en cours sauf pour l'exception spécifiée
    public void StopAllAudios(string name)
    {
        foreach (Audio audio in audios)
        {
            if (audio.clip.name == name)
                continue;

            if (audio.source.isPlaying)
                audio.source.Stop();
        }

        foreach (GameObject audio in listOfAudios)
        {
            AudioSource audioSource = audio.GetComponent<AudioSource>();

            if (audioSource.clip.name == name)
                continue;

            Destroy(audio.gameObject);
        }
    }

    // Stop absolument tous les audios d'un même type en cours
    public void StopAllAudiosByAudioType(Audio.AudioType audioType)
    {
        foreach (Audio audio in audios)
            if (audio.audioType == audioType && audio.source.isPlaying)
                audio.source.Stop();

        for (int i = 0; i < listOfAudios.Count; i++)
        {
            AudioSource audioSource = listOfAudios[i].GetComponent<AudioSource>();
            Audio audio = Array.Find(audios, a => a.clip.name == audioSource.clip.name);

            if (audio.audioType == audioType && audioSource.isPlaying)
            {
                Destroy(listOfAudios[i]);
                listOfAudios.RemoveAt(i);
                i--;
            }
        }
    }
    // Stop absolument tous les audios d'un même type en cours sauf pour l'exception spécifiée
    public void StopAllAudiosByAudioType(Audio.AudioType audioType, string name)
    {
        foreach (Audio audio in audios)
        {
            if (audio.clip.name == name)
                continue;
            if (audio.audioType == audioType && audio.source.isPlaying)
                audio.source.Stop();
        }

        for (int i = 0; i < listOfAudios.Count; i++)
        {
            AudioSource audioSource = listOfAudios[i].GetComponent<AudioSource>();
            Audio audio = Array.Find(audios, a => a.clip.name == audioSource.clip.name);

            if (audio.clip.name == name)
                continue;

            if (audio.audioType == audioType && audioSource.isPlaying)
            {
                Destroy(listOfAudios[i]);
                listOfAudios.RemoveAt(i);
                i--;
            }
        }
    }
}

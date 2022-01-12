using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    public enum AudioAction
    {
        START,
        RESTART,
    }

    [Serializable]
    public class AudioObject
    {
        public Audio.AudioType type;
        public AudioClip clip;
    }

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
        ClearAudios();

        if (stopAllAudios)
            StartCoroutine(StopAllAudios());
    }

    /******************Pour les GameObjects ayant des audiosources*******************/
    public void PlayAudio(string name, AudioSource[] audioSources, bool resettable)
    {

        AudioSource audioSource = Array.Find(audioSources, sound => sound.clip.name == name);

        // On rejoue l'audio m�me s'il �tait d�j� en cours ou si aucun n'�tait en cours
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
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    /***************Pour les audiosources sur l'AudioManager******************/
    public void PlayAudio(string name, Audio.AudioType audioType, AudioAction audioAction, bool stopSameAudioType = true)
    {
        Audio audio = Array.Find(audios, sound => sound.clip.name == name);
        switch (audioType)
        {
            // 1 thème à la fois
            case Audio.AudioType.BACKGROUND:
                // START si l'audio n'était pas en cours de lecture
                if (audioAction == AudioAction.START)
                {
                    // Stop tous le audios d'un même type sauf l'audio à jouer s'il était déjà en cours de lecture
                    //StopAllAudiosByAudioType(audioType, name);
                    if (!audio.source.isPlaying)
                        audio.source.Play();
                    break;
                }
                // RESTART si l'audio était déjà en cours de lecture
                StopAllAudiosByAudioType(audioType);
                audio.source.Play();
                break;

            // Plusieurs SFX possibles à la fois
            case Audio.AudioType.SFX:
                if (audioAction == AudioAction.START)
                {
                    if (stopSameAudioType)
                    {
                        StopAllAudiosByAudioType(audioType, name);
                        PlayAudio(name, audioType);
                        break;
                    }
                    // !stopSameAudioType
                    PlayAudio(name, audioType);
                    break;
                }
                // RESTART si l'audio était déjà en cours de lecture
                StopAudio(name, audioType);
                audio.source.Play();
                break;
            default:
                break;
        }
    }

    public void PlayAudio(string name, Audio.AudioType audioType)
    {
        Audio audio = Array.Find(audios, sound => sound.clip.name == name);

        GameObject audioGameObject = Instantiate(audioModel);
        audioGameObject.transform.parent = audiosParent.transform;

        AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();
        audioSource.clip = audio.clip;
        audioSource.volume = audio.volume;
        switch (audioType)
        {
            case Audio.AudioType.SFX:
                audioSource.pitch = Mathf.Clamp(UnityEngine.Random.Range(audio.pitch - 0.5f, audio.pitch + 0.5f), 0.5f, 3f);
                break;
            case Audio.AudioType.BACKGROUND:
                audioSource.pitch = audio.pitch;
                break;
        }


        audioSource.loop = audio.loop;
        audioSource.Play();

        listOfAudios.Add(audioGameObject);

        if (!audio.source.loop)
            Destroy(audioGameObject, audioSource.clip.length);
    }

    // Stop un audio en particulier d'un certain type
    public void StopAudio(string name, Audio.AudioType audioType)
    {
        Audio audio = Array.Find(audios, sound => sound.clip.name == name);

        if (audio.audioType == audioType)
            audio.source.Stop();
    }

    // Stop absolument tous les audios en cours
    public IEnumerator StopAllAudios()
    {
        foreach (Audio audio in audios)
            if (audio.source.isPlaying)
                audio.source.Stop();

        foreach (GameObject audio in listOfAudios)
            Destroy(audio.gameObject);

        yield return new WaitForEndOfFrame();
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
    public void ClearAudios()
    {
        for (int i = 0; i < listOfAudios.Count; i++)
            if (listOfAudios[i] == null)
            {
                listOfAudios.RemoveAt(i);
                i--;
            }
    }
}

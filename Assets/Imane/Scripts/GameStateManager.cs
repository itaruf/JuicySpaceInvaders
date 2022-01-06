using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    public bool isGameOver = false;

    public AudioSource[] audioSources;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*TEST AUDIO SOURCES SUR L'AUDIO MANAGER*/
        if (Input.GetKeyDown(KeyCode.T))
            // On peut le reset ; sera remplacé si un autre audio du même type veut être joué
            AudioManager.Instance.PlayAudio("BossMain", Audio.AudioType.BACKGROUND, true);

        if (Input.GetKeyDown(KeyCode.Y))
            AudioManager.Instance.PlayAudio("BossIntro", Audio.AudioType.BACKGROUND, true);

        if (Input.GetKeyDown(KeyCode.U))
            // On ne peut pas le reset ; instancie un prefab ; sera détruit automatiquement à la fin du clip 
            AudioManager.Instance.PlayAudio("Laser1", Audio.AudioType.SFX, false);

        if (Input.GetKeyDown(KeyCode.I))
            // On peut le reset ; sera détruit automatiquement à la fin du clip
            AudioManager.Instance.PlayAudio("Intro Jingle", Audio.AudioType.SFX, true);

        if (Input.GetKeyDown(KeyCode.O))
            AudioManager.Instance.StopAllAudiosByAudioType(Audio.AudioType.SFX);

        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(AudioManager.Instance.StopAllAudios());
        /**/

        /*TEST AUDIO SOURCES SUR L'OBJET ET NON L'AUDIO MANAGER*/
        if (Input.GetKeyDown(KeyCode.H))
            // Si on décide de rejouer cette musique, elle sera reset (true)
            AudioManager.Instance.PlayAudio(audioSources[0].clip.name, audioSources, true);

        if (Input.GetKeyDown(KeyCode.J))
            // Si on décide de rejouer cette musique, elle ne sera pas reset (false)
            AudioManager.Instance.PlayAudio(audioSources[1].clip.name, audioSources, false);

        if (Input.GetKeyDown(KeyCode.K))
            // Stop un audio en particulier
            AudioManager.Instance.StopAudio(audioSources[1].clip.name, audioSources);

        if (Input.GetKeyDown(KeyCode.L))
            // Stop tous les audios de l'objet
            AudioManager.Instance.StopAllAudios(audioSources);

        if (AudioManager.Instance.stopAllAudios)
            AudioManager.Instance.StopAllAudios(audioSources);
        /**/

        if (Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene("imane");

    }

    public void GameOver(bool value)
    {
        isGameOver = value;
    }

    public bool IsGameOver()
    {
        return (isGameOver);
    }
}

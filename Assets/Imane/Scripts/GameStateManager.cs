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
    public bool isWon = false;
    public float timeScaleIncrease = 1f;

    [HideInInspector] public AudioSource[] audioSources;

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

        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (EnemySpawnerManager.Instance.totalEnemies == 0)
        {
            isWon = true;
        }

        /*TEST AUDIO SOURCES SUR L'AUDIO MANAGER*/
        if (Input.GetKeyDown(KeyCode.T))
            AudioManager.Instance.PlayAudio("Theme", Audio.AudioType.BACKGROUND, AudioManager.AudioAction.START);

        if (Input.GetKeyDown(KeyCode.Y))
            AudioManager.Instance.PlayAudio("Theme", Audio.AudioType.BACKGROUND, AudioManager.AudioAction.RESTART);

        if (Input.GetKeyDown(KeyCode.U))
            AudioManager.Instance.PlayAudio("Laser1", Audio.AudioType.SFX, AudioManager.AudioAction.START,  false);

        if (Input.GetKeyDown(KeyCode.I))
            AudioManager.Instance.PlayAudio("Intro Jingle", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);

        if (Input.GetKeyDown(KeyCode.O))
            // On peut le reset ; sera détruit automatiquement à la fin du clip
            AudioManager.Instance.PlayAudio("Intro Jingle", Audio.AudioType.SFX, AudioManager.AudioAction.RESTART);
            //AudioManager.Instance.StopAllAudiosByAudioType(Audio.AudioType.SFX);

        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(AudioManager.Instance.StopAllAudios());
        /**/

        /*TEST AUDIO SOURCES SUR L'OBJET ET NON L'AUDIO MANAGER*/
        if (Input.GetKeyDown(KeyCode.H))
            // Si on decide de rejouer cette musique, elle sera reset (true)
            AudioManager.Instance.PlayAudio(audioSources[0].clip.name, audioSources, true);

        if (Input.GetKeyDown(KeyCode.J))
            // Si on decide de rejouer cette musique, elle ne sera pas reset (false)
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
        Time.timeScale = 1f;
        isGameOver = value;
    }

    public bool IsGameOver()
    {
        return (isGameOver);
    }
}

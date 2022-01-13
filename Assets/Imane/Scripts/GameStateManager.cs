using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public int currentScore;
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

    public GameObject OnDisableGameOver;
    public GameObject OnDisableGameOverUI;
    public GameObject OnGameOver;
    public GameObject OnVictory;

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
            StartCoroutine(Victory(true));
            isWon = true;
        }

        if (!AudioManager.Instance.stopAllAudios)
            AudioFirstLevel();

        /*TEST AUDIO SOURCES SUR L'AUDIO MANAGER*//*
        if (Input.GetKeyDown(KeyCode.T))
            AudioManager.Instance.PlayAudio("BossMain", Audio.AudioType.BACKGROUND, AudioManager.AudioAction.START);

        if (Input.GetKeyDown(KeyCode.Y))
            AudioManager.Instance.PlayAudio("BossMain", Audio.AudioType.BACKGROUND, AudioManager.AudioAction.RESTART);

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
        *//**/

            /*TEST AUDIO SOURCES SUR L'OBJET ET NON L'AUDIO MANAGER*//*
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

            *//**//*
            if (Input.GetKeyDown(KeyCode.N))
                SceneManager.LoadScene("imane");*/
    }

    public IEnumerator GameOver(bool value)
    {
        yield return new WaitForSeconds(1f);
        OnDisableGameOver.SetActive(false);
        OnDisableGameOverUI.SetActive(false);
        OnGameOver.SetActive(true);
        Time.timeScale = 1f;
        isGameOver = value;

        yield return new WaitWhile(() => !Input.anyKey);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Victory(bool value)
    {
        yield return new WaitForSeconds(1f);
        OnDisableGameOver.SetActive(false);
        OnDisableGameOverUI.SetActive(false);
        OnVictory.SetActive(true);
        Time.timeScale = 1f;
        isGameOver = value;

        yield return new WaitWhile(() => !Input.anyKey);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOver()
    {
        return (isGameOver);
    }

    public void AudioFirstLevel()
    {
        AudioManager.Instance.PlayAudio("Ambient", Audio.AudioType.BACKGROUND, AudioManager.AudioAction.START);
        AudioManager.Instance.PlayAudio("RainAmbient", Audio.AudioType.BACKGROUND, AudioManager.AudioAction.START);
    }
}

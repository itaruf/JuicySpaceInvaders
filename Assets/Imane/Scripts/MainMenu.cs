using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton, quitButton;
    private GameObject lastButtonSelected;
    void Start()
    {
        Cursor.visible = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;

        EventSystem.current.SetSelectedGameObject(playButton);
        lastButtonSelected = EventSystem.current.currentSelectedGameObject;

        StartCoroutine(PlayEffect());
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastButtonSelected);
        }
        else
        {
            lastButtonSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator PlayEffect()
    {
        RetroEffectManager.instance.cameraGlitch.SetFloat("StaticAmount", UnityEngine.Random.Range(0.1f, 0.5f));
        FindObjectOfType<ThunderManager>().ForceThunder();
        yield return new WaitForSecondsRealtime(2f);
        RetroEffectManager.instance.thunder.SetActive(false);
        RetroEffectManager.instance.cameraGlitch.SetFloat("StaticAmount", 0f);
        yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(5, 15));
        StartCoroutine(PlayEffect());
    }
}

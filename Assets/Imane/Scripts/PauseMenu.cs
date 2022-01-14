using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeButton, quitButton;
    private GameObject lastButtonSelected;
    private bool isPaused = false;
    public bool holdPauseMenu = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !holdPauseMenu)
        {
            if (isPaused)
                UnPause();
            else
                Pause();
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastButtonSelected);
        }
        else
        {
            lastButtonSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);
        lastButtonSelected = resumeButton;

        AudioListener.pause = true;
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        lastButtonSelected = null;

        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }

}

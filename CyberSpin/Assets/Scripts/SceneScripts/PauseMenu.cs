using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject tutorialsUI;

    public GameObject settingsMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused && !settingsMenuUI.activeSelf)
            {
                Resume();
                tutorialsUI.SetActive(true);
            }
            else if(!settingsMenuUI.activeSelf)
            {
                Pause();
                settingsMenuUI.SetActive(false);
                tutorialsUI.SetActive(false);
            }
            else if(settingsMenuUI.activeSelf)
            {
                settingsMenuUI.SetActive(false);
                Resume();
                tutorialsUI.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        LevelManager.instanceLevelManager.toNextPos();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        CursorDisable.disable = false;
        gameIsPaused = false;
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
    }

    private void Pause()
    {
        CursorDisable.disable = true;
        gameIsPaused = true;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        CursorDisable.disable = false;
        SceneManager.LoadScene("Menu");
    }

    public void Stats()
    {
        Debug.Log("STATS");
        //Time.timeScale = 1;
        //SceneManager.LoadScene("Stats");
    }
}

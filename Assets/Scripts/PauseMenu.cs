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
    public Animator crossFadeAnimator;


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
        StartCoroutine(SwitchToMenu());
    }

    public void Stats()
    {
        Debug.Log("STATS");
        //Time.timeScale = 1;
        //SceneManager.LoadScene("Stats");
    }


    private IEnumerator SwitchToMenu()
    {
        crossFadeAnimator.SetTrigger("SwitchScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}

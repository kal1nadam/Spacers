using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Animator sceneTransition;

    public Button playButton;
    public Button quitButton;


    public void Play()
    {
        sceneTransition.SetTrigger("SwitchScene");
        StartCoroutine(StartGame());
    }
    
    public void Quit()
    {
        Application.Quit();
    }


    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Game");
    }

}

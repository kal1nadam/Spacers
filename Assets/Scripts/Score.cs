using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    private TMP_Text scoreText;
    public static int currentScore;

    private bool updateScore;
    //values
    float updateDelay = 0.15f;
    int redViperEnemyKill_Value = 20;

    private void OnEnable()
    {
        RedViperENEMY.redViperKill += RedViperKill;
    }

    private void OnDisable()
    {
        RedViperENEMY.redViperKill -= RedViperKill;
    }

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        updateScore = true;
    }

    private void FixedUpdate()
    {
        if (updateScore)
        {
            scoreText.text = (Convert.ToInt32(scoreText.text) + 1).ToString();
            currentScore = Convert.ToInt32(scoreText.text);
            updateScore = false;
            StartCoroutine(CommonUpdateScore());
        }
    }

    private IEnumerator CommonUpdateScore()
    {
        yield return new WaitForSeconds(updateDelay);
        updateScore = true;
    }

    private void UpdateScore(int value)
    {
        scoreText.text = (Convert.ToInt32(scoreText.text) + value).ToString();
    }

    private void RedViperKill()
    {
        UpdateScore(redViperEnemyKill_Value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeValue; // a minute to beat the level
    public Text timeText;
    public int count = 0;
   
    void Start()
    {
        timeValue = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
        }
        else
        {
            GameOver();
            timeValue = 0;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = timeToDisplay % 1 * 1000;

        timeText.text = string.Format("TIme: {0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);

    }

    
}

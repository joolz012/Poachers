using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text minutesText;
    public Text secondsText;
    private float currentTime;
    public bool isRunning;
    public GameObject timerGameO;

    void Start()
    {
        isRunning = false;
    }

    void Update()
    {
        if (isRunning)
        {
            timerGameO.SetActive(true);
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                Debug.Log("Timer has reached zero!");
                ResetTimer();
            }

            UpdateTimerText();
        }
        else
        {
            timerGameO.SetActive(false);
        }
    }

    public void TimerDuration(float timer)
    {
        currentTime = timer * 60;
        isRunning = true;
    }

    void ResetTimer()
    {
        isRunning = false;
        timerGameO.SetActive(false);
        minutesText.text = "00";
        secondsText.text = "00";
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
    }


}

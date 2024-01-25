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
        // Load the timer from PlayerPrefs
    }

    void Update()
    {
        if (isRunning)
        {
            timerGameO.SetActive(true);
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                // Timer has reached zero, you can handle the timer completion here
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
        // Reset timer to the initial value
        isRunning = false;
        timerGameO.SetActive(false);
        minutesText.text = "00";
        secondsText.text = "00";
    }

    void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Update the UI text
        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
    }


}

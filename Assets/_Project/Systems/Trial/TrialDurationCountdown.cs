using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialDurationCountdown : Countdown
{
    public static Action trialEnded;

    private void OnEnable()
    {
        TrialStartCountdown.trialStarted += StartCountdown;
    }
    private void OnDisable()
    {
        TrialStartCountdown.trialStarted -= StartCountdown;
    }

    private void Update()
    {
        if (countdownStarted)
        {
            countdownText.text = (countdownTime - (int)timeElapsed).ToString();
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= countdownTime)
            {
                countdownStarted = false;
                timeElapsed = 0;

                trialEnded?.Invoke();
                TrialStartCountdown.trialStarted -= StartCountdown;
                Destroy(gameObject);
            }
        }
    }
}

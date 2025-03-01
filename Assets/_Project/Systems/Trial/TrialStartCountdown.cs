using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrialStartCountdown : Countdown
{
    public static Action trialStarted;

    private void Awake()
    {
        countdownText.enabled = false;
        if (countdownText == null)
        {
            countdownText = GetComponent<TextMeshProUGUI>();
        }
    }
    private void OnEnable()
    {
        SceneMover.SceneMoveFinished += StartCountdown;
    }

    private void OnDisable()
    {
        SceneMover.SceneMoveFinished -= StartCountdown;
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

                trialStarted?.Invoke();
                SceneMover.SceneMoveFinished -= StartCountdown;
                Destroy(gameObject);
            }
        }
    }
}

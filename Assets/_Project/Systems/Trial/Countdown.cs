using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] protected int countdownTime = 3;
    protected float timeElapsed = 0;
    protected bool countdownStarted = false;

    [SerializeField] protected TextMeshProUGUI countdownText;

    private void Awake()
    {
        if (countdownText == null)
        {
            countdownText = GetComponent<TextMeshProUGUI>();
        }
        countdownText.enabled = false;
    }
    protected virtual void StartCountdown()
    {
        countdownStarted = true;
        countdownText.enabled = true;
    }
}

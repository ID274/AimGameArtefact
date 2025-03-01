using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialPlayerToggler : MonoBehaviour
{
    private FPSController fpsController;
    private PlayerGun playerGun;

    private void Awake()
    {
        playerGun = GetComponent<PlayerGun>();
        fpsController = GetComponent<FPSController>();
    }

    private void OnEnable()
    {
        TrialStartCountdown.trialStarted += EnablePlayer;
        TrialDurationCountdown.trialEnded += DisablePlayer;

    }

    private void OnDisable()
    {
        TrialStartCountdown.trialStarted -= EnablePlayer;
        TrialDurationCountdown.trialEnded -= DisablePlayer;
    }

    private void EnablePlayer()
    {
        playerGun.enabled = true;
        fpsController.enabled = true;
    }

    private void DisablePlayer()
    {
        playerGun.enabled = false;
        fpsController.enabled = false;
    }
}

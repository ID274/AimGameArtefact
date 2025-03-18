using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostTrialInstructions : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [TextArea][SerializeField] private TextMeshProUGUI instructionsText;

    private void Awake()
    {
        instructionsText = instructionsPanel.GetComponentInChildren<TextMeshProUGUI>();
        instructionsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        TrialDurationCountdown.trialEnded += ShowInstructions;
    }

    private void OnDisable()
    {
        TrialDurationCountdown.trialEnded -= ShowInstructions;
    }

    private void ShowInstructions()
    {
        GameObject crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }

        if (SettingsPanel.firstRun)
        {
            instructionsText.text = $"Final score: {TrialScore.Score}. \n\nPlease restart the application to complete your second run.";
        }
        else
        {
            instructionsText.text = $"Final score: {TrialScore.Score}. \n\nThank you for participating!";
        }
        instructionsPanel.SetActive(true);
        //StartCoroutine(OpenDirectory(5));
    }

    private IEnumerator OpenDirectory(int secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);
        Application.OpenURL($"{ExperimentRecorder.folderPath}");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostTrialInstructions : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private TextMeshProUGUI instructionsText;

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
        instructionsText.text = "Please send the two files generated in this path to ID274@student.aru.ac.uk: \n" + ExperimentRecorder.folderPath;
        instructionsPanel.SetActive(true);
    }
}

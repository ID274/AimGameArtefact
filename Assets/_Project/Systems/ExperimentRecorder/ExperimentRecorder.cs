using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ExperimentRecorder : MonoBehaviour
{
    public static ExperimentRecorder Instance { get; private set; }

    [SerializeField] private List<ShotData> shotDataList = new List<ShotData>();

    private static Guid guid;

    public static string folderPath;
    public static string folderName;

    private void Awake()
    {
        guid = Guid.NewGuid();
        folderPath = Application.persistentDataPath;
        folderName = "ExperimentData";
        folderPath += $"/{folderName}";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        TrialDurationCountdown.trialEnded += SaveExperiment;
    }

    private void OnDisable()
    {
        TrialDurationCountdown.trialEnded -= SaveExperiment;
    }

    public void RecordShotData(ShotData shotData)
    {
        shotDataList.Add(shotData);
        Debug.Log($"Shot data recorded: Shot hit: {shotData.hit}, Time since last shot: {shotData.timeSinceLastShot}, Mouse Sensitivity: {shotData.sensitivity}, Crosshair Type: {shotData.crosshairType}, Crosshair Size: {shotData.crosshairSize}", this);
    }

    private void SaveExperiment()
    {
        SaveShotData();
        CreateAverageData();
    }

    private void SaveShotData()
    {
        string path = folderPath + $"/shotData_{DateTime.Now.ToString($"yyyyMMMdd_HHmmss")}_{guid.ToString()}.txt";
        string stringToSave = "";
        for (int i = 0; i < shotDataList.Count; i++)
        {
            stringToSave += $"Shot number: {i + 1},\nHit: {shotDataList[i].hit},\nTime since last shot: {shotDataList[i].timeSinceLastShot},\nMouse Sensitivity: {shotDataList[i].sensitivity},\nCrosshair Type: {shotDataList[i].crosshairType}\nCrosshair Size: {shotDataList[i].crosshairSize}\n\n";
        }

        System.IO.File.WriteAllText(path, stringToSave);
        Debug.Log($"Data saved to {path}");
    }

    private void CreateAverageData()
    {
        string path = folderPath + $"/averageData_{DateTime.Now.ToString($"yyyyMMMdd_HHmmss")}_{guid.ToString()}.txt";
        string stringToSave = "";
        float averageTimeSinceLastShot = 0;
        float sensitivity = shotDataList[0].sensitivity;
        CrosshairTypeEnum crosshairType = shotDataList[0].crosshairType;
        float crosshairSize = shotDataList[0].crosshairSize;
        float accuracy = 0;

        for (int i = 0; i < shotDataList.Count; i++)
        {
            averageTimeSinceLastShot += shotDataList[i].timeSinceLastShot;
            if (shotDataList[i].hit)
            {
                accuracy++;
            }
        }

        averageTimeSinceLastShot /= shotDataList.Count;
        accuracy /= shotDataList.Count;

        stringToSave = $"Total shots: {shotDataList.Count},\nMouse sensitivity: {sensitivity},\nCrosshair Type: {crosshairType},\nCrosshair Size: {crosshairSize},\nAverage accuracy: {accuracy},\nAverage time since last shot: {averageTimeSinceLastShot}";
        System.IO.File.WriteAllText(path, stringToSave);
        Debug.Log($"Data saved to {path}");
    }
}

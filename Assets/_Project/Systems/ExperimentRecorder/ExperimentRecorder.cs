using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExperimentRecorder : MonoBehaviour
{
    public static ExperimentRecorder Instance { get; private set; }

    [SerializeField] private List<ShotData> shotDataList = new List<ShotData>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void RecordShotData(ShotData shotData)
    {
        shotDataList.Add(shotData);
        Debug.Log($"Shot data recorded: Shot hit: {shotData.hit}, Time since last shot: {shotData.timeSinceLastShot}, Mouse Sensitivity: {shotData.sensitivity}", this);
    }

    private void OnApplicationQuit()
    {
        if (shotDataList.Count > 0)
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        string path = Application.persistentDataPath + $"/shotData{DateTime.Now.ToString($"YYYYMMMDD_HHmmss")}{$"_{Guid.NewGuid().ToString()}"}.txt";
        string stringToSave = "";
        for (int i = 0; i < shotDataList.Count; i++)
        {
            stringToSave += $"Shot number: {i},\nHit: {shotDataList[i].hit},\nTime since last shot: {shotDataList[i].timeSinceLastShot},\nMouse Sensitivity: {shotDataList[i].sensitivity}\n\n";
        }

        System.IO.File.WriteAllText(path, stringToSave);
        Debug.Log($"Data saved to {path}");
    }
}

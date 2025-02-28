using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }

    public SceneMover sceneMover;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneMover sceneMover = GetComponent<SceneMover>();
    }
    public void ChangeScene()
    {
        // Load next scene and wrap around if at the end
        int tempIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int nextSceneIndex = tempIndex >= SceneManager.sceneCountInBuildSettings ? 0 : tempIndex;
        Debug.Log($"Current scene index: {tempIndex - 1}, Next scene index: {nextSceneIndex}");
        sceneMover.SetUpForMove();
        StartCoroutine(LoadSceneAndMoveObjects(nextSceneIndex));
    }

    private IEnumerator LoadSceneAndMoveObjects(int nextSceneIndex)
    {
        // Load the next scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
        // Wait until the new scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // Move objects to the new scene
        sceneMover.MoveToTrialScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(MySceneManager))]
public class SceneMover : MonoBehaviour
{
    public static event Action SceneMoveFinished;
    public GameObject[] objectsToMove;
    public GameObject[] objectsMoved;
    public void MoveToTrialScene()
    {
        objectsMoved = new GameObject[objectsToMove.Length];
        foreach (GameObject objectToMove in objectsToMove)
        {
            SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetActiveScene());
            if (objectToMove.layer == 5)
            {
                Canvas canvas = FindAnyObjectByType<Canvas>();
                objectToMove.transform.SetParent(canvas.transform, false);
                objectToMove.transform.localPosition = Vector3.zero;
            }
            objectsMoved[Array.IndexOf(objectsToMove, objectToMove)] = objectToMove;
        }

        //SceneMoveCleaner sceneMoveCleaner = new SceneMoveCleaner();
        //sceneMoveCleaner.Clean(objectsMoved);
        //sceneMoveCleaner = null;

        SceneMoveFinished?.Invoke();
    }

    public void SetUpForMove()
    {
        foreach (GameObject objectToMove in objectsToMove)
        {
            objectToMove.transform.SetParent(null);
            DontDestroyOnLoad(objectToMove);
        }
    }
}

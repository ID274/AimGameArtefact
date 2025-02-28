using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MySceneManager))]
public class SceneMover : MonoBehaviour
{
    public GameObject[] objectsToMove;
    public void MoveToTrialScene()
    {
        foreach (GameObject objectToMove in objectsToMove)
        {
            SceneManager.MoveGameObjectToScene(objectToMove, SceneManager.GetActiveScene());
            if (objectToMove.layer == 5)
            {
                Canvas canvas = FindAnyObjectByType<Canvas>();
                objectToMove.transform.SetParent(canvas.transform, false);
                objectToMove.transform.localPosition = Vector3.zero;
            }
        }
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

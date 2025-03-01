using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveCleaner
{
    public void Clean(GameObject[] objects)
    {
        foreach (GameObject objectToMove in objects)
        {
            if (objectToMove.TryGetComponent<FPSController>(out var fpsController))
            {
                fpsController.enabled = true;
                fpsController.GetComponent<PlayerGun>().enabled = true;
            }
        }
    }
}

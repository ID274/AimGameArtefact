using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMainCamera : MonoBehaviour
{
    private static Camera mainCamera;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        RotateToCamera();
    }

    private void RotateToCamera()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
}

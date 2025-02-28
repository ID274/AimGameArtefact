using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private const string destructibleTag = "Destructible";

    private Camera mainCamera;

    [Header("Gun Settings")]
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private bool shotReady = true;

    private float timeSinceLastShot = 0;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        shotReady = timeSinceLastShot >= minTimeBetweenShots ? true : false;
        if (Input.GetMouseButtonDown(0) && shotReady)
        {
            Shoot();
        }
        timeSinceLastShot += Time.deltaTime;
    }
    private void Shoot()
    {
        Debug.Log($"Shot fired at {Time.time}");
        bool shotHit = false;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(destructibleTag))
            {
                Debug.Log($"Hit {hit.collider.gameObject.name}");
                Destroy(hit.collider.gameObject);
                shotHit = true;
            }
        }
        if (!shotHit)
        {
            Debug.Log("Shot missed");
        }

        if (ExperimentRecorder.Instance != null)
        {
            ExperimentRecorder.Instance.RecordShotData(new ShotData(shotHit, timeSinceLastShot, FPSController.Sensitivity));
        }
        timeSinceLastShot = 0;
    }
}

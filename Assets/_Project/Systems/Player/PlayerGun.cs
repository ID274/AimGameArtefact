using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private const string destructibleTag = "Destructible";

    private Camera mainCamera;
    private SoundPlayer soundPlayer;

    [Header("Gun Settings")]
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private bool shotReady = true;

    private float timeSinceLastShot = 0;

    private void Awake()
    {
        mainCamera = Camera.main;
        soundPlayer = GetComponent<SoundPlayer>();
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
        soundPlayer.PlaySound("Gunshot");
        Debug.Log($"Shot fired at {Time.time}");
        bool shotHit = false;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(destructibleTag))
            {
                Debug.Log($"Hit {hit.collider.gameObject.name}");
                if (hit.collider.gameObject.TryGetComponent(out PlaySoundOnDeath deathSound))
                {
                    deathSound.PlayDestroySound();
                }
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
            ExperimentRecorder.Instance.RecordShotData(new ShotData(shotHit, timeSinceLastShot, FPSController.Sensitivity, CrosshairChanger.crosshairType, CrosshairChanger.crosshairSize, CameraSettings.currentFOV));
        }
        timeSinceLastShot = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelObject;
    [SerializeField] private FPSController playerController;
    [SerializeField] private PlayerGun playerGun;

    [Header("Sensitivity")]
    [SerializeField] private Slider sensitivitySlider;

    [SerializeField] private Image crosshair;

    [Header("Crosshair")]
    [SerializeField] private CrosshairChanger crosshairChanger;

    private void Start()
    {
        StartCoroutine(TogglePanelOnStart());
        sensitivitySlider.minValue = FPSController.minSensitivity;
        sensitivitySlider.maxValue = FPSController.maxSensitivity;
        sensitivitySlider.value = FPSController.Sensitivity;
        LoadPlayerPrefsFloat();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TogglePanel();
        }
    }

    private IEnumerator TogglePanelOnStart()
    {
        yield return new WaitForSeconds(0.1f);
        TogglePanel();
        TogglePanel(); // we turn it off and on again to ensure the player can't shoot while its open
    }

    private void OnEnable()
    {
        CameraSettings.OnFOVChange += HandleFOVChange;
    }

    private void OnDisable()
    {
        CameraSettings.OnFOVChange -= HandleFOVChange;
    }

    private void TogglePanel()
    {
        panelObject.SetActive(!panelObject.activeSelf);
        playerController.enabled = !panelObject.activeSelf;
        playerGun.enabled = !panelObject.activeSelf;
        Cursor.lockState = panelObject.activeSelf ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = panelObject.activeSelf;

        if (!panelObject.activeSelf)
        {
            SaveSettings();
        }
    }

    private void SaveSettings()
    {
        // Update mouse sensitivity
        FPSController.Sensitivity = sensitivitySlider.value;

        // Update crosshair colour
        crosshair.color = crosshairChanger.ReturnCurrentColor();

        // Update crosshair size
        crosshair.rectTransform.sizeDelta = crosshairChanger.ReturnCurrentSize();

        crosshair.overrideSprite = crosshairChanger.ReturnCurrentSprite();

        SavePlayerPrefsFloat("Sensitivity");
    }

    private void HandleFOVChange()
    {
        SavePlayerPrefsFloat("FOV");
    }

    public void Continue()
    {
        SaveSettings();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MySceneManager.Instance.ChangeScene();
    }

    private void SavePlayerPrefsFloat(string prefToSave)
    {
        float value = 0;
        
        switch (prefToSave)
        {
            case "Sensitivity":
                value = sensitivitySlider.value;
                break;
            case "FOV":
                value = CameraSettings.currentFOV;
                break;
        }

        PlayerPrefs.SetFloat(prefToSave, value);
    }

    private void LoadPlayerPrefsFloat()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.interactable = false; // disable slider to ensure same sensitivity between playthroughs
        }
        else
        {
            SavePlayerPrefsFloat("Sensitivity");
        }

        if (PlayerPrefs.HasKey("FOV"))
        {
            CameraSettings camSettings = FindObjectOfType<CameraSettings>();

            if (camSettings != null)
            {
                camSettings.fovSlider.interactable = false;
                camSettings.fovSlider.value = PlayerPrefs.GetFloat("FOV");
            }
            else
            {
                Debug.LogError("CameraSettings script not found in scene");
            }
        }
        else
        {
            SavePlayerPrefsFloat("FOV");
        }
    }
}
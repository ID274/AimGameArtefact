using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelObject;
    [SerializeField] private FPSController playerController;

    [Header("Sensitivity")]
    [SerializeField] private Slider sensitivitySlider;

    [SerializeField] private Image crosshair;

    [Header("Crosshair")]
    [SerializeField] private CrosshairChanger crosshairChanger;

    private void Start()
    {
        panelObject.SetActive(false);
        sensitivitySlider.minValue = playerController.minSensitivity;
        sensitivitySlider.maxValue = playerController.maxSensitivity;
        sensitivitySlider.value = playerController.Sensitivity;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        panelObject.SetActive(!panelObject.activeSelf);
        playerController.enabled = !panelObject.activeSelf;
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
        playerController.Sensitivity = sensitivitySlider.value;

        // Update crosshair colour
        crosshair.color = crosshairChanger.ReturnCurrentColor();

        // Update crosshair size
        crosshair.rectTransform.sizeDelta = crosshairChanger.ReturnCurrentSize();
    }
}
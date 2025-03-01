using System.Collections;
using System.Collections.Generic;
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
        panelObject.SetActive(false);
        sensitivitySlider.minValue = FPSController.minSensitivity;
        sensitivitySlider.maxValue = FPSController.maxSensitivity;
        sensitivitySlider.value = FPSController.Sensitivity;
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
    }

    public void Continue()
    {
        SaveSettings();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MySceneManager.Instance.ChangeScene();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    private Camera _camera;
    public static int currentFOV = 60;
    private const int minFOV = 40;
    private const int maxFOV = 120;

    [SerializeField] private Slider fovSlider;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        fovSlider.minValue = minFOV;
        fovSlider.maxValue = maxFOV;
        fovSlider.value = 60;
    }

    public void ChangeFOV()
    {
        _camera.fieldOfView = fovSlider.value;
        currentFOV = (int)fovSlider.value;
    }
}

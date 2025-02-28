using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// FPSController is based on https://gist.github.com/KarlRamstedt/407d50725c7b6abeaf43aee802fdd88e

public class FPSController : MonoBehaviour
{
    public static float Sensitivity
    {
        get { return sensitivity; }
        set
        {
            value = Mathf.Clamp(value, minSensitivity, maxSensitivity);
            sensitivity = value;
        }
    }
    [Header("Mouse Movement")]
    private static float sensitivity;
    [SerializeField] public static float minSensitivity { get; private set; }
    [SerializeField] public static float maxSensitivity { get; private set; }

    [Header("Mouse Rotation")]
    [SerializeField] private float yRotationLimit = 80.0f;
    private Vector2 rotation = Vector2.zero;

    [Header("HUD Canvas")]
    [SerializeField] private Canvas hudCanvas;

    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    public bool useAspectScaling = true;
    private float sensitivityScale = 1f;

    private void Awake()
    {
        minSensitivity = 1.0f;
        maxSensitivity = 10.0f;
        Sensitivity = 5.5f;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float referenceWidth = 1920f;  // Your reference resolution width
        float aspectRatio = hudCanvas.GetComponent<RectTransform>().sizeDelta.x / referenceWidth;

        sensitivityScale = useAspectScaling ? aspectRatio : 1f;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity * sensitivityScale;
        rotation.y += Input.GetAxis(yAxis) * sensitivity * sensitivityScale;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        transform.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotation.y, Vector3.left);
    }
}
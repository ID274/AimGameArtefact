using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        ToggleButton();
    }
    public void ToggleButton()
    {
        continueButton.SetActive(toggle.isOn);
    }
}

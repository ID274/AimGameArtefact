using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private void Start()
    {
        UpdateSliderText();
    }
    public void UpdateSliderText()
    {
        if (slider.wholeNumbers)
        {
            sliderText.text = slider.value.ToString();
        }
        else
        {
            sliderText.text = slider.value.ToString("F2");
        }
    }
}

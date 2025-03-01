using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairChanger : MonoBehaviour
{
    private const int alpha = 255;

    [Header("Sliders")]
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Slider sizeSlider;

    [Header("Preview Image")]
    [SerializeField] private Sprite crosshairSprite;
    [SerializeField] private Image crosshairPreview;

    [Header("Crosshair Types")]
    [SerializeField] private Sprite[] crosshairSprites;
    [SerializeField] private TMP_Dropdown crosshairDropdown;


    private void Awake()
    {
        redSlider.value = 255;
        greenSlider.value = 255;
        blueSlider.value = 255;
        sizeSlider.value = 1;
    }

    private void Start()
    {
        UpdateCrosshairType();
        UpdateCrosshairColour();
        UpdateCrosshairSize();
    }

    public void UpdateCrosshairColour()
    {
        crosshairPreview.color = NewCrosshairColor();
    }

    public void UpdateCrosshairSize()
    {
        crosshairPreview.rectTransform.sizeDelta = NewCrosshairSize();
    }

    public void UpdateCrosshairType()
    {
        crosshairPreview.overrideSprite = crosshairSprites[crosshairDropdown.value];
    }

    private Color NewCrosshairColor()
    {
        Color color = new Color(redSlider.value / 255, greenSlider.value / 255, blueSlider.value / 255, alpha);
        return color;
    }

    private Vector2 NewCrosshairSize()
    {
        Vector2 sizeDelta = new Vector2(100 * sizeSlider.value, 100 * sizeSlider.value);
        return sizeDelta;
    }

    public Color ReturnCurrentColor()
    {
        return NewCrosshairColor();
    }

    public Vector2 ReturnCurrentSize()
    {
        return NewCrosshairSize();
    }

    public Sprite ReturnCurrentSprite()
    {
        return crosshairPreview.overrideSprite;
    }
}

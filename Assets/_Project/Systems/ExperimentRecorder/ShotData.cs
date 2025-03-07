using System.Collections;

public struct ShotData
{
    public bool hit;
    public float timeSinceLastShot;
    public float sensitivity;
    public CrosshairTypeEnum crosshairType;
    public float crosshairSize;
    public int fieldOfView;

    public ShotData(bool hit, float timeSinceLastShot, float sensitivity, CrosshairTypeEnum crosshairType, float crosshairSize, int fieldOfView)
    {
        this.hit = hit;
        this.timeSinceLastShot = timeSinceLastShot;
        this.sensitivity = sensitivity;
        this.crosshairType = crosshairType;
        this.crosshairSize = crosshairSize;
        this.fieldOfView = fieldOfView;
    }
}
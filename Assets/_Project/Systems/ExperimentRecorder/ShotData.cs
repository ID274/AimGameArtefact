using System.Collections;

public struct ShotData
{
    public bool hit;
    public float timeSinceLastShot;
    public float sensitivity;

    public ShotData(bool hit, float timeSinceLastShot, float sensitivity)
    {
        this.hit = hit;
        this.timeSinceLastShot = timeSinceLastShot;
        this.sensitivity = sensitivity;
    }
}
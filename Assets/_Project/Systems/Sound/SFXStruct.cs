using UnityEngine;

[System.Serializable]
public struct SFXStruct
{
    public string name;
    public AudioClip audioClip;

    public SFXStruct(string name, AudioClip audioClip)
    {
        this.name = name;
        this.audioClip = audioClip;
    }
}

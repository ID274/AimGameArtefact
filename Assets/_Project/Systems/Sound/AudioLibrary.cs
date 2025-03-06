using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    public static AudioLibrary Instance { get; private set; }

    public SFXStruct[] audioClips;

    [SerializeField] private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (SFXStruct audioClip in audioClips)
        {
            AddSound(audioClip.name, audioClip.audioClip);
        }
    }

    public void AddSound(string soundName, AudioClip audioClip)
    {
        if (!audioDictionary.ContainsKey(soundName))
        {
            audioDictionary.Add(soundName, audioClip);
        }
        else
        {
            Debug.LogWarning("Sound with name " + soundName + " already exists in the dictionary");
        }
    }

    public void RemoveSound(string soundName)
    {
        audioDictionary.Remove(soundName);
    }

    public AudioClip GrabSound(string soundName)
    {
        if (!audioDictionary.ContainsKey(soundName))
        {
            Debug.LogWarning("Sound with name " + soundName + " does not exist in the dictionary");
            return null;
        }
        return audioDictionary[soundName];
    }
}
